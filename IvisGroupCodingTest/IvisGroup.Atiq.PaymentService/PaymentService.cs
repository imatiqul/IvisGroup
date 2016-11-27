using System;
using System.Diagnostics;

namespace IvisGroup.Atiq.PaymentService
{
    public class PaymentService
    {
        private IPaymentFactory paymentFactory;
        private ITimer timer = null;

        /// <summary>
        /// Added new constructor to pass Timer along with Service
        /// </summary>
        /// <param name="service">IPaymentFactory</param>
        /// <param name="timer">ITimer</param>
        public PaymentService(IPaymentFactory service, ITimer timer): this(service)
        {
            this.timer = timer;
        }

        public PaymentService(IPaymentFactory service)
        {
            this.paymentFactory = service;
        }
        public void TakePayment(PaymentDetails details)
        {
            if (timer != null)
            {
                timer.Start();
            }
            var payment = paymentFactory.CreatePayment();
            payment.Authorise(details);
            if (timer != null)
            {
                var time = timer.Elapsed();
            }
        }
    }
    public interface IPaymentFactory
    {
        Payment CreatePayment();
    }
    public class Payment : IPayment
    {
        public void Authorise(PaymentDetails details)
        {
            // some code that does some stuff
        }
    }
    public interface IPayment
    {
        void Authorise(PaymentDetails details);
    }
    public class PaymentDetails { }

    /// <summary>
    /// Timer Interface
    /// </summary>
    public interface ITimer {
        void Start();
        long Elapsed(); //Time in milliseconds
    }

    public class Timer : ITimer
    {
        private Stopwatch _stopwatch;

        public Timer()
        {
            _stopwatch = new Stopwatch();
        }

        public void Start()
        {
            if (_stopwatch.IsRunning)
            {
                _stopwatch.Restart();
            }
            else
            {
                _stopwatch.Start();
            }
        }

        public long Elapsed()
        {
            if (!_stopwatch.IsRunning)
            {
                throw new Exception("Timer has not been started!");
            }

            return _stopwatch.ElapsedMilliseconds;
        }
    }
}
