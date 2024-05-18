using JustinaSystem.DAL;
using JustinaSystem.Entities;
using JustinaSystem.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustinaSystem.BLL
{
    public class DogServices
    {
        private readonly JustinaContext _justinaContext;
        private readonly IMemoryCache _memoryCache;

        internal DogServices(IMemoryCache memoryCache, JustinaContext justinaContext)
        {
            _justinaContext = justinaContext;
            _memoryCache = memoryCache;
        }

        //View list of dogs for a customer
        public List<DogView> ViewDogs(int customerID)
        {
            // a container to hold x number of exceptions messages
            List<Exception> errorList = new List<Exception>();

            // Create a list to store DogView objects
            List<DogView> customerDogs = new List<DogView>();

            // Check if customerID is greater than 0
            if (customerID > 0)
            {
                // Filter the Dogs based on the customerID
                return customerDogs = _justinaContext.Dogs
                    .Where(x => x.customer_id == customerID)
                    .Select(d => new DogView
                    {
                        DogId = d.dog_id,
                        CustomerId = d.customer_id,
                        DogName = d.dog_name,
                        Breed = d.breed,
                        DateOfBirth = d.date_of_birth,
                        Sex = d.sex,
                        Vet = d.vet,
                        Weight = (float)d.weight,
                        Color = d.color,
                        OkWithOtherDogs = d.ok_with_other_dogs,
                        HasAllergies = d.has_allergies,
                        OnMedication = d.on_medication,
                        TakeApik = d.take_apik,
                        Notes = d.notes
                    })
                    .ToList();
            }

            return new List<DogView>();
        }

        //Add a dog to a customer
        public void AddDog(DogView dog)
        {
            // a container to hold x number of exceptions messages
            List<Exception> errorList = new List<Exception>();

            // Chcek for an existing dog to avaid duplicates
            bool isDuplicate = _justinaContext.Dogs.Any(x => x.dog_name == dog.DogName &&
                                        x.customer_id == dog.CustomerId &&
                                        x.breed == dog.Breed &&
                                        x.date_of_birth == dog.DateOfBirth);

            if (isDuplicate)
            {
                errorList.Add(new Exception($"There is an exiting dog with Dog name {dog.DogName}, customerId {dog.CustomerId}, {dog.Breed} and {dog.DateOfBirth}"));
            }

            Dog newDog = new Dog();

            //Check if incoming parameter has a value
            if (dog == null)
            {
                throw new ArgumentNullException("No dog to be added");
            }

            //Chcek if all the fiels have a value
            if (string.IsNullOrWhiteSpace(dog.DogName))
            {
                errorList.Add(new Exception("Dog name is missing"));
            }
            if (string.IsNullOrWhiteSpace(dog.Breed))
            {
                errorList.Add(new Exception("Dog breed data is missing"));
            }
            if (dog.DateOfBirth == DateTime.MinValue)
            {
                errorList.Add(new Exception("The birthdate is missing"));
            }

            //Check if the customer exits
            Customer customer = _justinaContext.Customers
                                    .Where(x => x.customer_id == dog.CustomerId).FirstOrDefault();
            if (customer == null)
            {
                errorList.Add(new Exception("The customer does not exist"));
            }

            //if no error at this point then create a dog

            if (errorList.Count == 0)
            {
                newDog.customer_id = dog.CustomerId;
                newDog.dog_name = dog.DogName;
                newDog.breed = dog.Breed;
                newDog.date_of_birth = dog.DateOfBirth;
                newDog.sex = dog.Sex;
                newDog.vet = dog.Vet;
                newDog.weight = dog.Weight;
                newDog.color = dog.Color;
                newDog.ok_with_other_dogs = dog.OkWithOtherDogs;
                newDog.has_allergies = dog.HasAllergies;
                newDog.on_medication = dog.OnMedication;
                newDog.take_apik = dog.TakeApik;
                newDog.notes = dog.Notes;

            }

            // Add the created dog to the dog table
            _justinaContext.Dogs.Add(newDog);

            //if there noe errors, save the changes
            if (errorList.Count > 0)
            {
                throw new AggregateException("Unable to create dog. Chcek concerns", errorList.OrderBy(x => x.Message).ToList());
            }

            else
            {
               _justinaContext.SaveChanges();

            }



        }

        //View a single dog
        public DogView ViewASingleDog(int dogId)
        {
            Dog dog = new Dog();

            dog = _justinaContext.Dogs.FirstOrDefault(x => x.dog_id == dogId);

            if (dog is null)
            {
                return null;
            }

            return new DogView()
            {
                DogId = dog.dog_id,
                CustomerId = dog.customer_id,
                DogName = dog.dog_name,
                Breed = dog.breed,
                DateOfBirth = dog.date_of_birth,
                Sex = dog.sex,
                Vet = dog.vet,
                Weight = (float)dog.weight,
                Color = dog.color,
                OkWithOtherDogs = dog.ok_with_other_dogs,
                HasAllergies = dog.has_allergies,
                OnMedication = dog.on_medication,
                TakeApik = dog.take_apik,
                Notes = dog.notes

            };
        }

        // Counting the number of dogs for a customer
        public int CountDogs(int customerId)
        {
            return _justinaContext.Dogs.Where(x => x.customer_id == customerId).Count();
        }

        // View a list of all dogs
        public async Task<List<DogView>> ViewAllDogs()
        {
            // Get dogs list from cache
            List<DogView> cachedDogs = _memoryCache.Get<List<DogView>>("AllDogs");

            // Check if the dogs list is not cached
            if (cachedDogs == null)
            {
                // If not cached, fetch dogs from the database
                List<Dog> dogs = await _justinaContext.Dogs.ToListAsync();

                // Map Dog entities to DogView objects
                cachedDogs = dogs.Select(d => new DogView
                {
                    DogId = d.dog_id,
                    CustomerId = d.customer_id,
                    DogName = d.dog_name,
                    Breed = d.breed,
                    DateOfBirth = d.date_of_birth,
                    Sex = d.sex,
                    Vet = d.vet,
                    Weight = (float)d.weight,
                    Color = d.color,
                    OkWithOtherDogs = d.ok_with_other_dogs,
                    HasAllergies = d.has_allergies,
                    OnMedication = d.on_medication,
                    TakeApik = d.take_apik,
                    Notes = d.notes,
                    Owner = $"{d.customer.first_name} {d.customer.last_name}"
                }).ToList();

                // Cache the dogs list with a sliding expiration of 1 hour
                _memoryCache.Set("AllDogs", cachedDogs, new MemoryCacheEntryOptions
                {
                    SlidingExpiration = TimeSpan.FromMinutes(1)
                });
            }

            return cachedDogs;
        }


    }
}
