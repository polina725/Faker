namespace Faker.TestClasses
{
    public class A
    {
        private int number;
        private int n;
        public string str;

        public int Number { set { number = value; } }
        public string S { set; get; }

        public A(int num)
        {
            number = num;
        }

        public override string ToString()
        {
            return number + " " + n + " " + S + " " + str;
        }
    }
}
