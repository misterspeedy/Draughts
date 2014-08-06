[<NUnit.Framework.TestFixture>]
module BoardTests

open System
open NUnit.Framework
open Board
open Square
open Piece

[<Test>]
let ``The board is 8x8``() =
   let sut = EmptyBoard()
   let expected = 8, 8
   let actual = sut.Width, sut.Height
   Assert.AreEqual(expected, actual)

[<Test>]
let ``I can access any square in the board by column and row``() =
   let sut = EmptyBoard()
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
   let sut = EmptyBoard()
   let cant = (fun () -> sut.[col, row] |> ignore)
   Assert.That(cant, Throws.TypeOf<System.IndexOutOfRangeException>())

[<Test>]
let ``I cannot update a square``() =
   // Test is inexpressible because ImmutableArray2D does not have an item setter
   let expected = true
   let actual = true
   Assert.AreEqual(expected, actual)

[<Test>]
let ``Odd numbered squares are unreachable``() =
   let isOdd c r = 
      let index = r * 8 + c
      index % 2 = 0
   let sut = EmptyBoard()
   let expected = Square.Unreachable
   for col in 0..sut.Width - 1 do
      for row in 0..sut.Height - 1 do
         if isOdd col row then 
            let actual = sut.[col, row]
            Assert.AreEqual(expected, actual)

[<Test>]
let ``Reachable squares are initially unoccupied``() =
   let isOdd r c = 
      let index = r * 8 + c
      index % 2 = 0
   let sut = EmptyBoard()
   let expected = Square.Unoccupied
   for col in 0..sut.Width - 1 do
      for row in 0..sut.Height - 1 do
         if not (isOdd row col) then 
            let actual = sut.[col, row]
            Assert.AreEqual(expected, actual)

[<Test>]
let ``I can place a piece in an unoccupied square producing a new board``() =
   let sut = EmptyBoard()
   let piece = Occupied(Piece)
   let sut' = sut.Set(1, 0, piece)
   Assert.AreNotEqual(sut, sut')

[<Test>]
let ``Placing a piece in an occupied square causes an error``() =
   let expected = true
   let actual = false
   Assert.AreEqual(expected, actual)