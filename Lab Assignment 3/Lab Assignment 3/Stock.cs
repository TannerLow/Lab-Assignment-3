using System;
using System.Collections.Generic;
using System.Text;

namespace Lab_Assignment_3
{
    class Stock
    {
        private string _name;
        private double _initialValue, _maximumChange, _notificationThreshold, _currentValue;

        public Stock(string name, double initialValue, double maximumChange, double notificationThreshold)
        {
            _name = name;
            _initialValue = initialValue;
            _maximumChange = maximumChange;
            _notificationThreshold = notificationThreshold;
        }

        public string Name
        {
            get => _name;
        }

        public double InitialValue
        {
            get => _initialValue;
        }

        public double MaximumChange
        {
            get => _maximumChange;
        }

        public double NotificationThreshold
        {
            get => _notificationThreshold;
        }

        public double CurrentValue
        {
            get => _currentValue;
        }
    }
}
