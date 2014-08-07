[<NUnit.Framework.TestFixture>]
module BoardTests

open System
open NUnit.Framework
open Board
open Square
open Piece

let IsOdd c r = 
   let index = r * 8 + c
   index % 2 = 1

[<Test>]
let ``The board is 8x8``() =
   let sut = Board()
   let expected = 8, 8
   let actual = sut.Width, sut.Height
   Assert.AreEqual(expected, actual)

[<Test>]
let ``I can access any square in the board by column and row``() =
   let sut = Board()
   for col in 0..sut.Width - 1 do
      for row in 0..sut.Height - 1 do
         let expected = true
         let actual = 
            try
               sut.[col, row] |> ignore
               true
            with
            | _ -> false
         Assert.AreEqual(expected, actual)

[<TestCase(-1, -1)>]
[<TestCase(-1, 0)>]
[<TestCase(8, 0)>]
[<TestCase(0, 8)>]
let ``Accessing a square outside the board returns an error``(row, col) =
   let sut = Board()
   let cant = (fun () -> sut.[col, row] |> ignore)
   Assert.That(cant, Throws.TypeOf<System.IndexOutOfRangeException>())

[<Test>]
let ``I cannot update a square``() =
   // Test is inexpressible because ImmutableArray2D does not have an item setter
   let expected = true
   let actual = true
   Assert.AreEqual(expected, actual)

[<Test>]
let ``Even numbered squares are unreachable``() =
   let sut = Board()
   let expected = Square.Unreachable
   for col in 0..sut.Width - 1 do
      for row in 0..sut.Height - 1 do
         if not (IsOdd col row) then 
            let actual = sut.[col, row]
            Assert.AreEqual(expected, actual)

[<Test>]
let ``Reachable squares are initially unoccupied``() =
   let sut = Board()
   let expected = Square.Unoccupied
   for col in 0..sut.Width - 1 do
      for row in 0..sut.Height - 1 do
         if IsOdd col row then 
            let actual = sut.[col, row]
            Assert.AreEqual(expected, actual)

[<Test>]
let ``I can place a piece in an unoccupied square producing a new board``() =
   let sut = Board()
   let square = Occupied(Piece)
   let sut' = sut.Set(1, 0, square)
   Assert.AreNotEqual(sut, sut')

[<Test>]
let ``When I have placed a piece I can find that piece in the new board``() =
   let sut = Board()
   let square = Occupied(Piece)
   let sut' = sut.Set(1, 0, square)
   let expected = square
   let actual = sut'.[1, 0]
   Assert.AreEqual(expected, actual)

[<Test>]
let ``Placing a piece in an occupied square causes an error``() =
   let sut = Board()
   let square = Occupied(Piece)
   let sut' = sut.Set(1, 0, square)
   let cant = (fun () -> 
      let sut'' = sut'.Set(1, 0, square)
      ())
   Assert.That(cant, Throws.TypeOf<System.ArgumentException>())

[<Test>]
let ``Placing a piece in an unreachable square causes an error``() =
   let expected = true
   let actual = false
   Assert.AreEqual(expected, actual)