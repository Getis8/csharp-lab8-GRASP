# Завдання 5: Метрики CBO та LCOM4

Метрики рахуємо вручну за правилами умови: типи домену й інфраструктури проєкту рахуються, примітиви й `System.*` ні (тому `Console`, `List`, `Enum`, `ArgumentException` не входять).

## CBO до рефакторингу

`LegacyLogisticsProcessor` (метод `ProcessShipmentConsole`) пов'язаний з типами проєкту: `Sender`, `Cargo`, `Shipment`. **CBO = 3** (системні `Console`, `string` не рахуємо).

> **Зауваження:** за формальним правилом CBO legacy-класу невеликий, бо весь код і всі залежності зосереджені в ньому, а класів у проєкті мало. Проблема legacy не в числі зв'язків, а в LCOM і змішанні обов'язків. Це варто зазначити на захисті. Якщо викладач очікує бачити вищий CBO, пояснення таке: легасі-код поєднує з'єднання з консоллю, сховищем і email у тілі методу напряму, а такі «залежності» (Console, виведення «реєстр», «email») там не виражені типами проєкту, тож метрика їх не бачить.

## CBO після рефакторингу

| Клас | Залежності (типи проєкту) | CBO |
|---|---|---|
| **Sender** | немає | 0 |
| **Cargo** | DeliveryType | 1 |
| **ITariffPolicy** | Cargo, DeliveryType | 2 |
| **StandardTariffPolicy** | ITariffPolicy, Cargo, DeliveryType | 3 |
| **ExpressTariffPolicy** | ITariffPolicy, Cargo, DeliveryType, StandardTariffPolicy | 4 |
| **Shipment** | Sender, Cargo, ITariffPolicy | 3 |
| **ShipmentController** | IShipmentRepository, ITrackingNotifier, ITariffPolicyResolver, CreateShipmentCommand, ShipmentResult, Sender, Cargo, Shipment, DeliveryType | 9 |
| **DictionaryTariffResolver** | ITariffPolicyResolver, ITariffPolicy, DeliveryType | 3 |
| **ShipmentConsoleUi** | ShipmentController, ConsoleCommandParser, WaybillFormatter, ShipmentResult | 4 |

> **Спостереження:** `ShipmentController` має CBO = 9, що перевищує норматив ≤ 4. Це не слід приховувати. Його можна знизити (і це хороший матеріал для захисту):
> 1. Створення `Sender` і `Cargo` винести у фабрику запиту, наприклад `ShipmentFactory` (Pure Fabrication) або у статичний метод `Shipment.CreateFrom(command...)`. Тоді контролер більше не знає про `Sender`, `Cargo`, `DeliveryType`.
> 2. Після цього залишиться 7 залежностей: `IShipmentRepository`, `ITrackingNotifier`, `ITariffPolicyResolver`, `ShipmentFactory`, `CreateShipmentCommand`, `ShipmentResult`, `Shipment`.
> 3. Якщо об'єднати репозиторій і нотифікатор у один обробник події «відправку створено» (Indirection), буде 6, а якщо `Shipment` прихований за `ShipmentFactory` і `ShipmentResult.From(shipment)`, то 5.
> 
> *Реалістично досягти ≤ 4 можна лише з агресивним розбиттям, і це компроміс між Low Coupling і простотою (KISS).*

## LCOM4

Рахуємо за умовою: вершини — це методи, ребро між двома методами, якщо вони використовують спільне поле або один викликає інший.

| Клас | Методи | Зв'язність | LCOM4 |
|---|---|---|---|
| **LegacyLogisticsProcessor** | 1 метод `ProcessShipmentConsole`, що робить 5 різних речей | Формально 1 метод, 1 компонент, але змішані обов'язки; на рівні логічних блоків (діалог, розрахунок, накладна, реєстр, email) блоки не поділяють стан | формально 1, змістовно ≥ 4 |
| **Shipment** | Create, Dispatch, TotalCost, SenderCompanyName, SenderEmail, DeliveryType | Усі використовують Sender, Cargo або Status/витрати; TotalCost ← TransportFee/InsuranceFee, Dispatch ← Status; Create викликає конструктор, що пов'язує всі поля | 1 |
| **Cargo** | Create, CalculateInsurance | CalculateInsurance ← DeclaredValue, Create ← конструктор усіх полів | 1 |
| **Sender** | Create | Один метод | 1 |
| **ShipmentController** | CreateShipment | Один публічний метод, усі поля (репозиторій, нотифікатор, резолвер) в ньому | 1 |
| **InMemoryShipmentRepository**| NextTrackingId, Save, GetAll | NextTrackingId ← `_lastId`; Save та GetAll ← `_shipments`; ці дві групи не поділяють спільне поле | 2 |

> **Зауваження:** `InMemoryShipmentRepository` має LCOM4 = 2, бо генерація ідентифікатора і зберігання працюють з різними полями. Це можна виправити, винісши `NextTrackingId` в окремий `ITrackingIdGenerator` (Pure Fabrication), і тоді репозиторій матиме LCOM4 = 1. Або ж залишити, але в звіті пояснити як компроміс заради простоти (YAGNI для демо).

## Підсумкова таблиця для docs/METRICS.md

| Клас | CBO до | CBO після | LCOM4 до | LCOM4 після | Ціль |
|---|---|---|---|---|---|
| **Процесор / ShipmentController**| 3 | 9 → 5-7 (після `ShipmentFactory`) | ≥ 4 (змістовно) | 1 | CBO ≤ 4, LCOM4 = 1 |
| **Shipment** | 0 (анемічний) | 3 | 1 (без логіки) | 1 | виконано |
| **Cargo** | 0 | 1 | 1 | 1 | виконано |
| **Sender** | 0 | 0 | 1 | 1 | виконано |
| **InMemoryShipmentRepository**| н/д | 3 | н/д | 2 → 1 (після винесення генератора) | LCOM4 = 1 |

*Інструмент:* Точні CBO можна зняти в Visual Studio (Analyze → Calculate Code Metrics → «Class Coupling»). У VS Code його немає, тому рахуємо вручну за формулою з умови.

## Висновки

1. Рефакторинг перетворив один змішаний метод на набір класів з єдиною відповідальністю; доменні сутності мають CBO ≤ 4 і LCOM4 = 1.
2. Контролер має найвищий CBO, оскільки він за визначенням координатор. Знизити можна фабрикою, але це компроміс між Low Coupling і простотою.
3. Напрямок залежностей: `Cli` → `Application` → `Domain`, домен не залежить від нікого (I ≈ 0), CLI нестабільний (I ≈ 1), що відповідає принципу Stable Dependencies.