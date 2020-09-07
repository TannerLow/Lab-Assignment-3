using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;


namespace Lab_Assignment_3
{
    public class StockBroker
    {
        public string BrokerName { get; set; }
        public List<Stock> stocks = new List<Stock>();
        public static ReaderWriterLockSlim myLock = new ReaderWriterLockSlim();
        readonly string docPath = @"C:\Users\Documents\CECS 475\Lab3_output.txt";
        public string titles = "Broker".PadRight(10) + "Stock".PadRight(15) +
       "Value".PadRight(10) + "Changes".PadRight(10) + "Date and Time";
        /// <summary>
        /// The stockbroker object
        /// </summary>
        /// <param name="brokerName">The stockbroker's name</param>
        public StockBroker(string brokerName)
        {
            BrokerName = brokerName;
        }
        /// <summary>
        /// Adds stock objects to the stock list
        /// </summary>
        /// <param name="stock">Stock object</param>
        public void AddStock(Stock stock)
        {
            stocks.Add(stock);
            stock.StockEvent += EventHandler; 
        }
        /// <summary>
        /// The eventhandler that raises the event of a change
        /// </summary>
        /// <param name="sender">The sender that indicated a change</param>
        /// <param name="e">Event arguments</param>
        void EventHandler(Object sender, EventArgs e)
        {
            try
            {
                Stock newStock = (Stock)sender;
                string statement;
                string name = newStock.GetStockName.ToString();
                string value = newStock.GetStockValue.ToString();
                string numChanges = newStock.getNumChanges.ToString();
                statement = BrokerName + name + value + numChanges;
                Console.WriteLine(statement);

            }
            catch(Exception E) { Console.WriteLine(E.Message);}
        }    
    }
}