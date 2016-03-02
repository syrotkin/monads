using System;

namespace Monads {
    
    public abstract class Validation<L, A> {

        private readonly A m_value;

        public A Value {
            get {
                return m_value;
            }
        }

        public abstract L Left {
            get;
        }

        protected Validation(A value) {
            m_value = value;
        }

        public abstract Validation<L, B> Map<B>(Func<A, B> mapper);

        public abstract Validation<L, B> Bind<B>(Func<A, Validation<L, B>> mapper);

        public abstract bool IsSuccess();
    }

}