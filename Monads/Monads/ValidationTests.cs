using System;

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
                Age = 20,
                Name = "john"
            };
            var validation = ValidateAge(p);
            ProcessValidationResult("Age", validation);
            validation = ValidateName(p);
            ProcessValidationResult("Name", validation);
        }

        private static void ProcessValidationResult(string description,
                Validation<string, Person> validation) {
            if (!validation.IsSuccess()) {
                var failure = validation as Failure<string, Person>;
                if (failure != null) {
                    Console.WriteLine("{0} validation error", description);
                    Console.WriteLine(failure.Left);
                }
            } else {
                Console.WriteLine("{0} validation successful", description);
            }
        }

    }
}
