namespace SingleAndMultiThreading
{
    public class Worker
    {
        private readonly long _numOfObjToCreate;
        public bool IsDone;

        public Worker(long numOfObjToCreate)
        {
            _numOfObjToCreate = numOfObjToCreate;
        }

        public void DoWork()
        {
            for (long i = 0; i < _numOfObjToCreate; i++)
            {
                new object();
            }

            IsDone = true;
        }
    }
}