﻿namespace WebApp.Services
{
    public class CounterService
    {
        public int Counter { get; private set; }

        public void RaiseCounter()
        {
            Counter++;
        }
    }
}
