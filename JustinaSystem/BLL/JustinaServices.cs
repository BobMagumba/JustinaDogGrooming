#nullable enable
using JustinaSystem.DAL;
using JustinaSystem.Entities;
using JustinaSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustinaSystem.BLL
{
    public class JustinaServices
    {
        #region Fields
        private readonly JustinaContext _justinaContext;
        #endregion
        internal JustinaServices(JustinaContext justinaContext)
        {
            _justinaContext = justinaContext;
        }

        // Creating customer data
        public void AddCustomer(CustomerView customer)
        {
            // a containe to hold x number of exception messagesd
            List<Exception> errorlist = new List<Exception>();


            Customer newCustomer = new Customer();

            //Check if in the incoming parameter has a value
            if (customer == null)
            {
                throw new ArgumentNullException("No customer to be added");
            }

            //Ensure that all fields have a value
            if (string.IsNullOrWhiteSpace(customer.FirstName))
            {
                errorlist.Add(new Exception("Customer first name is missing"));
            }

            if (string.IsNullOrWhiteSpace(customer.LastName))
            {
                errorlist.Add(new Exception("Customer Last name is missing"));
            }

            if (string.IsNullOrWhiteSpace(customer.PhoneNumber1))
            {
                errorlist.Add(new Exception("Customer phone number is missing"));
            }
            if (string.IsNullOrWhiteSpace(customer.Address))
            {
                errorlist.Add(new Exception("Customer address is missing"));
            }

            //check if the customer exists on the database

            Customer customerCheck = _justinaContext.Customers
                                    .Where(x => x.address == customer.Address || x.phone_number1 == customer.PhoneNumber1).FirstOrDefault();
            if (customerCheck != null)
            {
                errorlist.Add(new Exception($"Customer with Address {customer.Address} and phone number {customer.PhoneNumber1} exists"));
            }

            // if no errors at this point, create a customer

            if (errorlist.Count == 0)
            {
                newCustomer.first_name = customer.FirstName;
                newCustomer.last_name = customer.LastName;
                newCustomer.phone_number1 = customer.PhoneNumber1;
                newCustomer.phone_number2 = customer.PhoneNumber2;
                newCustomer.phone_number3 = customer.PhoneNumber3;
                newCustomer.email_address = customer.EmailAddress;
                newCustomer.enrollment_date = DateTime.Now;
                newCustomer.address = customer.Address;
                newCustomer.city = customer.City;
                newCustomer.state = customer.State;
                newCustomer.zip_code = customer.Zip;
                newCustomer.referral = customer.Referral;
                newCustomer.notes = customer.Notes;
                
            }

            //Add the created new customer to the customer table
            _justinaContext.Customers.Add(newCustomer);
            // If there no errors, save the changes
            if (errorlist.Count > 0)
            {
                //Clear the change tracker
                _justinaContext.ChangeTracker.Clear();

                throw new AggregateException("Unable to craete customer. Check concerns", errorlist.OrderBy(x => x.Message).ToList());
            }
            else
            {
                _justinaContext.SaveChanges();

            }


        }

        //Get all customers
        public List<CustomerView> ViewCustomerWithCustomerObject(CustomerView customer)
        {
            IQueryable<Customer> query = _justinaContext.Customers;

            if (customer.CustomerId > 0)
            {
                query = query.Where(x => x.customer_id == customer.CustomerId);
            }
            if (!string.IsNullOrWhiteSpace(customer.FirstName))
            {
                query = query.Where(x => x.first_name.Contains(customer.FirstName));
            }
            if (!string.IsNullOrWhiteSpace(customer.LastName))
            {
                query = query.Where(x => x.last_name.Contains(customer.LastName));
            }
            if (!string.IsNullOrWhiteSpace(customer.PhoneNumber1) ||
                !string.IsNullOrWhiteSpace(customer.PhoneNumber2) ||
                !string.IsNullOrWhiteSpace(customer.PhoneNumber3))
            {
                query = query.Where(x =>
                    x.phone_number1.Contains(customer.PhoneNumber1) ||
                    x.phone_number2.Contains(customer.PhoneNumber2) ||
                    x.phone_number3.Contains(customer.PhoneNumber3));
            }
            if (!string.IsNullOrWhiteSpace(customer.EmailAddress))
            {
                query = query.Where(x => x.email_address.Contains(customer.EmailAddress));
            }
            if (customer.EnrollmentDate != null)
            {
                query = query.Where(x => x.enrollment_date == customer.EnrollmentDate);
            }
            if (!string.IsNullOrWhiteSpace(customer.Address))
            {
                query = query.Where(x => x.address.Contains(customer.Address));
            }
            if (!string.IsNullOrWhiteSpace(customer.City))
            {
                query = query.Where(x => x.city.Contains(customer.City));
            }
            if (!string.IsNullOrWhiteSpace(customer.State))
            {
                query = query.Where(x => x.state.Contains(customer.State));
            }
            if (!string.IsNullOrWhiteSpace(customer.Zip))
            {
                query = query.Where(x => x.zip_code.Contains(customer.Zip));
            }
            if (!string.IsNullOrWhiteSpace(customer.Referral))
            {
                query = query.Where(x => x.referral.Contains(customer.Referral));
            }

            List<CustomerView> customerList = query.Select(c => new CustomerView
            {
                CustomerId = c.customer_id,
                FirstName = c.first_name,
                LastName = c.last_name,
                PhoneNumber1 = c.phone_number1,
                PhoneNumber2 = c.phone_number2,
                PhoneNumber3 = c.phone_number3,
                EmailAddress = c.email_address,
                EnrollmentDate = (DateTime)c.enrollment_date,
                Address = c.address,
                City = c.city,
                State = c.state,
                Zip = c.zip_code,
                Referral = c.referral,
                Notes = c.notes,
                NumberOfDogs = _justinaContext.Dogs.Where(x => x.customer_id == c.customer_id).Count()



            }).ToList();

            return customerList;
        }


        //Serach using any of the parameters
        public List<CustomerView> ViewCustomerUsingAntParameters(
         int customerId = 0,
         string firstName = null,
         string lastName = null,
         string phoneNumber = null,
         string emailAddress = null,
         DateTime? enrollmentDate = null,
         string address = null,
         string city = null,
         string state = null,
         string zip = null,
         string referral = null)
        {
            List<Customer> customerList = new List<Customer>();

            // Search all customers all customers
            customerList = _justinaContext.Customers.ToList();


            if (customerId > 0)
            {
                customerList = customerList.Where(x => x.customer_id == customerId).ToList();
            }
            if (!string.IsNullOrWhiteSpace(firstName))
            {
                customerList = customerList.Where(x => x.first_name.Contains(firstName)).ToList();
            }
            if (!string.IsNullOrWhiteSpace(lastName))
            {
                customerList = customerList.Where(x => x.last_name.Contains(lastName)).ToList();
            }
            if (!string.IsNullOrWhiteSpace(phoneNumber))
            {
                customerList = customerList.Where(x =>
                    x.phone_number1.Contains(phoneNumber) ||
                    x.phone_number2.Contains(phoneNumber) ||
                    x.phone_number3.Contains(phoneNumber)).ToList();
            }
            if (!string.IsNullOrWhiteSpace(emailAddress))
            {
                customerList = customerList.Where(x => x.email_address.Contains(emailAddress)).ToList();
            }
            if (enrollmentDate.HasValue)
            {
                customerList = customerList.Where(x => x.enrollment_date == enrollmentDate.Value.Date).ToList();
            }
            if (!string.IsNullOrWhiteSpace(address))
            {
                customerList = customerList.Where(x => x.address.Contains(address)).ToList();
            }
            if (!string.IsNullOrWhiteSpace(city))
            {
                customerList = customerList.Where(x => x.city.Contains(city)).ToList();
            }
            if (!string.IsNullOrWhiteSpace(state))
            {
                customerList = customerList.Where(x => x.state.Contains(state)).ToList();
            }
            if (!string.IsNullOrWhiteSpace(zip))
            {
                customerList = customerList.Where(x => x.zip_code.Contains(zip)).ToList();
            }
            if (!string.IsNullOrWhiteSpace(referral))
            {
                customerList = customerList.Where(x => x.referral.Contains(referral)).ToList();
            }

            // Create CustomerView objects and return them
            return customerList.Select(c => new CustomerView
            {
                CustomerId = c.customer_id,
                FirstName = c.first_name,
                LastName = c.last_name,
                PhoneNumber1 = c.phone_number1,
                PhoneNumber2 = c.phone_number2,
                PhoneNumber3 = c.phone_number3,
                EmailAddress = c.email_address,
                EnrollmentDate = (DateTime)c.enrollment_date,
                Address = c.address,
                City = c.city,
                State = c.state,
                Zip = c.zip_code,
                Referral = c.referral,
                Notes = c.notes,
                NumberOfDogs = _justinaContext.Dogs.Where(x => x.customer_id == c.customer_id).Count()
            }).OrderByDescending(x => x.EnrollmentDate).ToList();
        }

        //Get a single customer using customer id
        public CustomerView ViewCustomer(int customerID)
        {
            Customer customer = new Customer();

            customer = _justinaContext.Customers.FirstOrDefault(x => x.customer_id == customerID);

            if (customer is null)
            {
                return null;
            }

            return new CustomerView
            {
                CustomerId = customer.customer_id,
                FirstName = customer.first_name,
                LastName = customer.last_name,
                PhoneNumber1 = customer.phone_number1,
                PhoneNumber2 = customer.phone_number2,
                PhoneNumber3 = customer.phone_number3,
                EmailAddress = customer.email_address,
                EnrollmentDate = (DateTime)customer.enrollment_date,
                Address = customer.address,
                City = customer.city,
                State = customer.state,
                Zip = customer.zip_code,
                Referral = customer.referral,
                Notes = customer.notes
            };

        }



    }
}
