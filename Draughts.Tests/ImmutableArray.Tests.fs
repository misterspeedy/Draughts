[<NUnit.Framework.TestFixture>]
module ImmutableArrayTests

open System
open NUnit.Framework
open ImmutableArray

[<Test>]
let ``I can create a 2D immutable array based on empty data``() =
   let ok = TestDelegate(fun () -> ImmutableArray2D(Array2D.init 0 0 (fun _ _ -> 0)) |> ignore)
   Assert.DoesNotThrow(ok)

[<Test>]
let ``I can create a 2D immutable array based on some data``() =
   let ok = TestDelegate(fun () -> ImmutableArray2D(Array2D.init 4 5 (fun x y -> x + y)) |> ignore)
   Assert.DoesNotThrow(ok)

[<Test>]
let ``I can get a value from the array``() =
   let sut = ImmutableArray2D(Array2D.init 4 5 (fun x y -> x + y))
   let expected = 2 + 3
   let actual = sut.[2, 3]
   Assert.AreEqual(expected, actual)

[<Test>]
let ``I can set a value in the array and get a new array back with the new value``() =
   let sut = ImmutableArray2D(Array2D.init 4 5 (fun x y -> x + y))
   let sut' = sut.Set(2, 3, 99)
   let expected = 99
   let actual = sut'.[2, 3]
   Assert.AreEqual(expected, actual)

[<Test>]
let ``When I set a value the orginal array is unaffected``() =
   let sut = ImmutableArray2D(Array2D.init 4 5 (fun x y -> x + y))
   let sut' = sut.Set(2, 3, 99)
   let expected = 2 + 3
   let actual = sut.[2, 3]
   Assert.AreEqual(expected, actual)

[<Test>]
let ``I can get the width and height of the array``() =
   let sut = ImmutableArray2D(Array2D.init 4 5 (fun x y -> x + y))
   let expected = 4, 5
   let actual = sut.Width, sut.Height
   Assert.AreEqual(expected, actual)

