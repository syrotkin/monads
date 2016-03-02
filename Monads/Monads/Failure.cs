using System;

namespace Monads {

    public class Failure<L, A> : Validation<L, A> {

        private readonly L m_left;

        private Failure(A value,
                L left) : base(value) {
            m_left = left;
        }

        public override L Left {
            get {
                return m_left;
            }
        }

        public override Validation<L, B> Map<B>(Func<A, B> mapper) {
            return CreateFailure(Left, mapper(Value));
        }

        public override Validation<L, B> Bind<B>(Func<A, Validation<L, B>> mapper) {
            var nextValidation = mapper(Value);
            return nextValidation.IsSuccess()
                    ? CreateFailure(Left, nextValidation.Value)
                    : CreateFailure(nextValidation.Left, nextValidation.Value);
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