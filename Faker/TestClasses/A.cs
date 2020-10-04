namespace Faker.TestClasses
{
    public class A
    {
        private int number;
        private int n;
        private string s;
        public string str;

        public int Number { set { number = value; } }
        public string S { set { s = value; } }

        public A(int num)
        {
            number = num;
        }

        public override string ToString()
        {
            return number + " " + n + " " + s + " " + str;
        }
    }
}
