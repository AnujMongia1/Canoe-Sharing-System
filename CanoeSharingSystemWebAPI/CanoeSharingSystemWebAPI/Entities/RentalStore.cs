﻿namespace CanoeSharingSystemWebAPI.Entities
{
    public class RentalStore
    {
        public int Id { get; set; }
        public string? StoreName { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? Password { get; set; }
        public string? PhoneNumber  { get; set; }
        public string? Location { get; set; }
    }
}
