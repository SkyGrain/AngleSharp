﻿namespace AngleSharp.DOM.Css
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/margin
    /// </summary>
    sealed class CSSMarginProperty : CSSShorthandProperty, ICssMarginProperty
    {
        #region Fields

        readonly CSSMarginTopProperty _top;
        readonly CSSMarginRightProperty _right;
        readonly CSSMarginBottomProperty _bottom;
        readonly CSSMarginLeftProperty _left;

        #endregion

        #region ctor

        internal CSSMarginProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.Margin, rule)
        {
            _top = Get<CSSMarginTopProperty>();
            _right = Get<CSSMarginRightProperty>();
            _bottom = Get<CSSMarginBottomProperty>();
            _left = Get<CSSMarginLeftProperty>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the value for the top margin.
        /// </summary>
        public IDistance Top
        {
            get { return _top.Top; }
        }

        /// <summary>
        /// Gets the value for the right margin.
        /// </summary>
        public IDistance Right
        {
            get { return _right.Right; }
        }

        /// <summary>
        /// Gets the value for the bottom margin.
        /// </summary>
        public IDistance Bottom
        {
            get { return _bottom.Bottom; }
        }

        /// <summary>
        /// Gets the value for the left margin.
        /// </summary>
        public IDistance Left
        {
            get { return _left.Left; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            var list = value as CSSValueList ?? new CSSValueList(value);
            CSSValue top = null;
            CSSValue right = null;
            CSSValue bottom = null;
            CSSValue left = null;

            if (list.Length > 4)
                return false;

            for (int i = 0; i < list.Length; i++)
            {
                if (!_top.CanStore(list[i], ref top) &&
                    !_right.CanStore(list[i], ref right) &&
                    !_bottom.CanStore(list[i], ref bottom) &&
                    !_left.CanStore(list[i], ref left))
                    return false;
            }

            right = right ?? top;
            bottom = bottom ?? top;
            left = left ?? right;
            return _top.TrySetValue(top) && _right.TrySetValue(right) && _bottom.TrySetValue(bottom) && _left.TrySetValue(left);
        }

        #endregion
    }
}
