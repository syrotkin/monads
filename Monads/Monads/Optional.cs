using System;

namespace Monads {
    public class Optional<T> where T : class {

        public static Optional<T> Empty = new Optional<T>(null);

        private readonly T m_value;

        public Optional(T value) {
            m_value = value;
        }

        public T Value {
            get {
                return m_value;
            }
        }
        
        /// <summary>
        /// "FlatMap". Defines Monad's policy for function composition
        /// </summary>
        public Optional<TB> Bind<TB>(Func<T, Optional<TB>> function) where TB : class {
            if (m_value == null) {
                return Optional<TB>.Empty;
            }
            return function(m_value);
        }

        /// <summary>
        /// Defines Monad's policy for function application
        /// </summary>
        public Optional<TB> Map<TB>(Func<T, TB> function) where TB : class {
            return Bind(x => new Optional<TB>(function(x)));
        }

    }

    public class Person {
        public Optional<Car> Car {
            get;
            set;
        }

        public string Name {
            get;
            set;
        }

        public int Age {
            get;
            set;
        }
    }

    public class Car {

        public Optional<Insurance> Insurance {
            get;
            set;
        }
    }

    public class Insurance {
        public string Name {
            get;
            set;
        }
    }

}
