using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;

namespace Stock
{
    public class StockBroker
    {
        public string BrokerName { get; set; }
        public List<Stock> stocks = new List<Stock>();
        public static readonly string title = "Broker".PadRight(10) + "Stock".PadRight(15) +
                "Value".PadRight(10) + "Changes".PadRight(10);
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
            Stock newStock = sender as Stock;
            string statement;
            string name = newStock.StockName;
            string value = newStock.CurrentValue.ToString();
            string numChanges = newStock.NumChanges.ToString();
            statement = BrokerName.PadRight(10) + name.PadRight(15) + value.PadRight(10) + numChanges.PadRight(10);
            Console.WriteLine(statement);
        }
    }

}
