using System;
using System.Runtime.InteropServices;

namespace Monads {

    public class ValidationTests {

        public Validation<string, Person> ValidateAge(Person p) {
            if (p.Age > 0 && p.Age < 130) {
                return Success<string, Person>.CreateSuccess<string, Person>(p);
            }
            return Failure<string, Person>.CreateFailure("Age must be between 0 and 130", p);
        }

        public Validation<string, Person> ValidateName(Person p) {
            if (char.IsUpper(p.Name[0])) {
                return Success<string, Person>.CreateSuccess<string, Person>(p);
            }
            return Failure<string, Person>.CreateFailure("Name must start with an uppercase character.", p);
        }

        public void Run() {
            var p = new Person {
                Age = -1,
                Name = "John"
            };
            var validation = ValidateAge(p);
            var validation2 = validation.Bind(ValidateName);
            ProcessValidationResult(validation);
            //validation = ValidateName(p);
            //ProcessValidationResult("Name", validation);
        }

        private static void ProcessValidationResult(Validation<string, Person> validation) {
            if (!validation.IsSuccess()) {
               Console.WriteLine("validation error");
                Console.WriteLine(validation.Left);
            } else {
                Console.WriteLine("validation successful");
            }
        }

    }
}
