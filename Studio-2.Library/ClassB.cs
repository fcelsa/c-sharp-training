namespace Studio_2.Library
{
    public class ClassB
    {
        public void AccessModifiers()
        {
            var c = new AccessClass();
            //c.MPrivate();
            c.MPublic();
            //c.MProtected();
            c.MInternal();
            //c.MProtectedPrivate();
            c.MProtectedInternal();
        }

        public bool GetRandom()
        {
            return Random.Shared.Next(0, 2) == 1;
        }
    }
}