namespace Lab_8;

public sealed class Sender
{
    public int Id { get; }
    public string CompanyName { get; }
    public string ContactPerson { get; }
    public string Email { get; }

    private Sender(int id, string companyName, string contactPerson, string email)
    {
        Id = id;
        CompanyName = companyName;
        ContactPerson = contactPerson;
        Email = email;
    }

    public static Sender Create(int id, string companyName, string contactPerson, string email)
    {
        if (id <= 0)
            throw new ArgumentException("Ідентифікатор відправника має бути додатним.", nameof(id));
        if (string.IsNullOrWhiteSpace(companyName))
            throw new ArgumentException("Назва компанії обов'язкова.", nameof(companyName));
        if (string.IsNullOrWhiteSpace(email) || !email.Contains('@'))
            throw new ArgumentException("Некоректний email відправника.", nameof(email));

        return new Sender(id, companyName.Trim(), contactPerson.Trim(), email.Trim());
    }
}