﻿using System;

namespace Monads {

    public class Success<L, A> : Validation<L, A> {

        private Success(A value) : base(value) {   
        }

        public override Validation<L, B> Map<B>(Func<A, B> mapper) {
            return CreateSuccess<L, B>(mapper(m_value));
        }

        public override Validation<L, B> Bind<B>(Func<A, Validation<L, B>> mapper) {
            return mapper(m_value);
        }

        public override bool IsSuccess() {
            return true;
        }

        // Originally "success" or "Unit"
        public static Success<L, T1> CreateSuccess<L, T1>(T1 value) {
            return new Success<L, T1>(value);
        }

    }
}