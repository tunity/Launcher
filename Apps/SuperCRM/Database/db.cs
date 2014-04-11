using Starcounter;

namespace SuperCRM
{

    [Database]
    public class Company
    {
        public string Name;
        public decimal Revenue;
    }

    [Database]
    public class Person
    {
        public string FirstName;
        public string LastName;
    }

    [Database]
    public class Contact
    {
        public Company Company;
        public Person Person;
        public string Email;
        public string Title;
    }

}
