using System;

namespace Monads {

    public class Failure<L, A> : Validation<L, A> {

        private readonly L m_left;

        private Failure(A value,
                L left) : base(value) {
            m_left = left;
        }

        public L Left {
            get {
                return m_left;
            }
        }

        public override Validation<L, B> Map<B>(Func<A, B> mapper) {
            return CreateFailure(Left, mapper(m_value));
        }

        public override Validation<L, B> Bind<B>(Func<A, Validation<L, B>> mapper) {
            var result = mapper(m_value);
            return result.IsSuccess()
                    ? CreateFailure(Left, result.Value)
                    : CreateFailure(((Failure<L, B>)result).Left, result.Value);
        }

        public override bool IsSuccess() {
            return false;
        }

        // originally "failure" or "Unit"
        public static Failure<L, T1> CreateFailure<L, T1>(L left, T1 value) {
            return new Failure<L, T1>(value, left);
        } 


    }

}