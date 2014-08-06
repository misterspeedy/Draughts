[<NUnit.Framework.TestFixture>]
module BoardTests

open System
open NUnit.Framework
open Board

[<Test>]
let ``The board is 8x8``() =
   let sut = EmptyBoard()
   let expected = 8, 8
   let actual = sut |> Array2D.length1, sut |> Array2D.length2
   Assert.AreEqual(expected, actual)

[<Test>]
let ``I can access any square in the board by column and row``() =
   let sut = EmptyBoard()
   for col in 0..sut |> Array2D.length1 do
      for row in 0..sut |> Array2D.length2 do
         let expected = None
         let actual = sut.[3,4]
         Assert.AreEqual(expected, actual)

[<TestCase(-1, -1)>]
[<TestCase(-1, 0)>]
[<TestCase(8, 0)>]
[<TestCase(0, 8)>]
let ``Accessing a square outside the board returns an error``(row, col) =
   let sut = EmptyBoard()
   let cant = (fun () -> sut.[row, col] |> ignore)
   Assert.That(cant, Throws.TypeOf<System.IndexOutOfRangeException>())

[<Test>]
let ``I cannot update a square``() =
   let expected = true
   let actual = false
   Assert.AreEqual(expected, actual)

[<Test>]
let ``Odd numbered squares are unreachable``() =
   let expected = true
   let actual = false
   Assert.AreEqual(expected, actual)

[<Test>]
let ``Even numbered squares are reachable``() =
   let expected = true
   let actual = false
   Assert.AreEqual(expected, actual)

[<Test>]
let ``Reachable squares might be unoccupied``() =
   let expected = true
   let actual = false
   Assert.AreEqual(expected, actual)

[<Test>]
let ``Reachable squares might be occupied by a piece``() =
   let expected = true
   let actual = false
   Assert.AreEqual(expected, actual)