namespace Faker.TestClasses
{
    public class A
    {
        private int number;
        private int n;
        public string str { get; set; }

        public int Number { get; }
        public string S { set; get; }

        public A(int num,int number)
        {
            number = num;
            this.Number = num;
        }

        public override string ToString()
        {
            return number + " " + n + " " + S + " " + str;
        }
    }
}
