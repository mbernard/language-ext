﻿using LanguageExt;
using System.Diagnostics.Contracts;
using static LanguageExt.TypeClass;
using static LanguageExt.Prelude;
using LanguageExt.Instances;
using LanguageExt.TypeClasses;

public static class NewTypeExtensions
{
    /// <summary>
    /// Append the NewType(x) to NewType(y)
    /// </summary>
    /// <param name="lhs">Left-hand side of the operation</param>
    /// <param name="rhs">Right-hand side of the operation</param>
    /// <returns>lhs + rhs</returns>
    [Pure]
    public static NEWTYPE Append<NEWTYPE, SEMI, A>(this NewType<NEWTYPE, A> lhs, NewType<NEWTYPE, A> rhs) 
        where SEMI    : struct, Semigroup<A>
        where NEWTYPE : NewType<NEWTYPE, A> =>
        from x in lhs
        from y in rhs
        select append<SEMI, A>(x, y);

    /// <summary>
    /// Add the bound values of x and y, uses an Add type-class to provide the add
    /// operation for type A.  For example x.Add<Metres, TInt, int>(y)
    /// </summary>
    /// <typeparam name="ADD">Add of A</typeparam>
    /// <typeparam name="A">Bound value type</typeparam>
    /// <param name="x">Left hand side of the operation</param>
    /// <param name="y">Right hand side of the operation</param>
    /// <returns>An NewType with y added to x</returns>
    [Pure]
    public static NEWTYPE Add<NEWTYPE, ADD, A>(this NewType<NEWTYPE, A> lhs, NewType<NEWTYPE, A> rhs)
        where ADD     : struct, Addition<A>
        where NEWTYPE : NewType<NEWTYPE, A> =>
        from x in lhs
        from y in rhs
        select add<ADD, A>(x, y);

    /// <summary>
    /// Find the difference between the two bound values of x and y, uses a Difference type-class 
    /// to provide the difference operation for type A.  For example x.Difference<TInteger,int>(y)
    /// </summary>
    /// <typeparam name="DIFF">Difference of A</typeparam>
    /// <typeparam name="A">Bound value type</typeparam>
    /// <param name="x">Left hand side of the operation</param>
    /// <param name="y">Right hand side of the operation</param>
    /// <returns>An NewType with the difference between x and y</returns>
    [Pure]
    public static NEWTYPE Difference<NEWTYPE, DIFF, A>(this NewType<NEWTYPE, A> x, NewType<NEWTYPE, A> y) 
        where DIFF : struct, Difference<A>
        where NEWTYPE : NewType<NEWTYPE, A> =>
        from a in x
        from b in y
        select difference<DIFF, A>(a, b);

    /// <summary>
    /// Find the product between the two bound values of x and y, uses a Product type-class 
    /// to provide the product operation for type A.  For example x.Product<TInteger,int>(y)
    /// </summary>
    /// <typeparam name="PROD">Product of A</typeparam>
    /// <typeparam name="A">Bound value type</typeparam>
    /// <param name="x">Left hand side of the operation</param>
    /// <param name="y">Right hand side of the operation</param>
    /// <returns>An option with the product of x and y</returns>
    [Pure]
    public static NEWTYPE Product<NEWTYPE, PROD, A>(this NewType<NEWTYPE, A> x, NewType<NEWTYPE, A> y) 
        where PROD : struct, Product<A>
        where NEWTYPE : NewType<NEWTYPE, A> =>
        from a in x
        from b in y
        select product<PROD, A>(a, b);

    /// <summary>
    /// Divide the two bound values of x and y, uses a Divide type-class to provide the divide
    /// operation for type A.  For example x.Divide<TDouble,double>(y)
    /// </summary>
    /// <typeparam name="DIV">Divide of A</typeparam>
    /// <typeparam name="A">Bound value type</typeparam>
    /// <param name="x">Left hand side of the operation</param>
    /// <param name="y">Right hand side of the operation</param>
    /// <returns>An option x / y</returns>
    [Pure]
    public static NEWTYPE Divide<NEWTYPE, DIV, A>(this NewType<NEWTYPE, A> x, NewType<NEWTYPE, A> y) 
        where DIV     : struct, Divisible<A>
        where NEWTYPE : NewType<NEWTYPE, A> =>
        from a in x
        from b in y
        select divide<DIV, A>(a, b);

    public static A Sum<NEWTYPE, NUM, A>(this NewType<NEWTYPE, A> self)
        where NEWTYPE : NewType<NEWTYPE, A>
        where NUM : Num<A> =>
        self.Value;

    public static int Sum<NEWTYPE>(this NewType<NEWTYPE, int> self) where NEWTYPE : NewType<NEWTYPE, int> =>
        self.Sum<NEWTYPE, TInt, int>();

    public static double Sum<NEWTYPE>(this NewType<NEWTYPE, double> self) where NEWTYPE : NewType<NEWTYPE, double> =>
        self.Sum<NEWTYPE, TDouble, double>();

    public static float Sum<NEWTYPE>(this NewType<NEWTYPE, float> self) where NEWTYPE : NewType<NEWTYPE, float> =>
        self.Sum<NEWTYPE, TFloat, float>();

    public static decimal Sum<NEWTYPE>(this NewType<NEWTYPE, decimal> self) where NEWTYPE : NewType<NEWTYPE, decimal> =>
        self.Sum<NEWTYPE, TDecimal, decimal>();
}
