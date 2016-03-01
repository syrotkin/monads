using System;

namespace Monads {
    class Program {
        static void Main() {
            
            var validationTests = new ValidationTests();
            validationTests.Run();
         
            var optionalTests = new OptionalTests();
            optionalTests.Run();

            Console.WriteLine("Done. Press any key...");
            Console.ReadKey();
        }
    }
}
