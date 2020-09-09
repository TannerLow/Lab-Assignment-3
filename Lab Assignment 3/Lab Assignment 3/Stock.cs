using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
namespace Stock
{
    public class Stock
    {
        // Declare EventHandler of type Stock Notification
        public event EventHandler<StockNotification> StockEvent;

        // Declare ReaderWriterLockSlim to lock writes to a shared resource from multiple threads.
        // Writes to a text file will include date & time, stock name, stock initial value, and stock price.
        public static ReaderWriterLockSlim myLock = new ReaderWriterLockSlim();
        public static readonly string docPath = @"C:\Users\Tanner\Documents\Github\Lab-Assignment-3\Lab Assignment 3\Lab Assignment 3\Lab3_output.txt";
        public static readonly string title = "Date and Time".PadRight(30) + "Stock".PadRight(15) +
                "Initial Value".PadRight(15) + "Price";
        private readonly Thread _thread;

        // Member variables. 
        public string StockName { get; set; }
        public int InitialValue { get; set; }
        public int CurrentValue { get; set; }
        public int MaxChange { get; set; }
        public int Threshold { get; set; }
        public int NumChanges { get; set; }

        /// <summary>
        /// Stock class that contains all the information and changes of the stock
        /// </summary>
        /// <param name="name">Stock name</param>
        /// <param name="startingValue">Starting stock value</param>
        /// <param name="maxChange">The max value change of the stock</param>
        /// <param name="threshold">The range for the stock</param>
        public Stock(string name, int startingValue, int maxChange, int threshold)
        {
            StockName = name;
            InitialValue = startingValue;
            CurrentValue = startingValue;
            MaxChange = maxChange;
            Threshold = threshold;
            _thread = new Thread(new ThreadStart(Activate));
            _thread.Start();
            this.StockEvent += FileEventHandler;
        }
        /// <summary>
        /// Activates the threads synchronizations
        /// </summary>
        public void Activate()
        {
            for (int i = 0; i < 25; i++)
            {
                Thread.Sleep(500); // 1/2 second
                ChangeStockValue();
                StockEvent(this, null);
            }
        }
        /// <summary>
        /// Changes the stock value and also raising the event of stock value changes
        /// </summary>
        public void ChangeStockValue()
        {
            var rand = new Random();
            CurrentValue += rand.Next(0, MaxChange+1);
            NumChanges++;
            if ((CurrentValue - InitialValue) > Threshold)
            {
                StockEvent?.Invoke(this, null);
            }
        }
        /// <summary>
        ///  The eventhandler that saves a stock's information to an output file.
        /// </summary>
        /// <param name="sender">The sender that indicated a change</param>
        /// <param name="e">Event arguments</param>
        protected void FileEventHandler(object sender, EventArgs e)
        {
            myLock.EnterWriteLock();
            Stock newStock = sender as Stock;
            DateTime date = DateTime.Now;
            using (System.IO.StreamWriter outputFile = new System.IO.StreamWriter(docPath, true))
            {
                outputFile.WriteLine(date.ToString().PadRight(30) + newStock.StockName.PadRight(15) +
                            newStock.InitialValue.ToString().PadRight(15) + newStock.CurrentValue.ToString());
            }
            myLock.ExitWriteLock();
        }
    }
}
