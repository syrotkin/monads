using System;
using System.Collections.Generic;
using System.Linq;

namespace Monads {
    class Program {
        static void Main() {
            var persons = GetPersons();
            var names = persons.Select(GetCarInsuranceName);
            foreach (var name in names) {
                Console.WriteLine(name);
            }
            Console.WriteLine("Done. Press any key...");
            Console.ReadKey();
        }
        
        private static string GetCarInsuranceName(Optional<Person> optionalPerson) {
            return optionalPerson
                .Bind(p => p.Car)
                .Bind(c => c.Insurance)
                .Map(i => i.Name)       // OR: .Bind(i => new Optional<string>(i.Name))
                .Value ?? "Unknown";
        }

        // Create test data
        private static IEnumerable<Optional<Person>> GetPersons() {
            return new List<Optional<Person>> {

                new Optional<Person>(new Person {
                    Car = new Optional<Car>(new Car {
                        Insurance = new Optional<Insurance>(new Insurance {
                            Name = "Axa"
                        })
                    })
                }),

                new Optional<Person>(new Person {
                    Car = new Optional<Car>(new Car {
                        Insurance = new Optional<Insurance>(new Insurance())
                    })
                }),

                new Optional<Person>(new Person {
                    Car = new Optional<Car>(new Car {
                        Insurance = new Optional<Insurance>(null)
                    })
                }),

                new Optional<Person>(new Person {
                    Car = new Optional<Car>(null)
                }),

                new Optional<Person>(null),
            };
        }
    }
}
