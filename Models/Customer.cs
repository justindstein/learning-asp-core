﻿namespace learning_asp_core.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }

        public string? CustomerName { get; set; }

        public Customer() { }

        public Customer(int customerId, string customerName)
        {
            CustomerId = customerId;
            CustomerName = customerName;
        }
    }
}