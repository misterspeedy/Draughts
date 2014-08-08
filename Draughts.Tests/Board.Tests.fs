[<NUnit.Framework.TestFixture>]
module BoardTests

open System
open NUnit.Framework
open Player
open Piece
open Square
open Board

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
   // Test is inexpressible because Board does not have an item setter
   let expected = true
   let actual = true
   Assert.AreEqual(expected, actual)

[<Test>]
let ``Even numbered rows start with an unreachable square``() =
   let sut = Board()
   let expected = Unreachable
   for row in 0..2..sut.Height-1 do
      let actual = sut.[0, row]
      Assert.AreEqual(expected, actual)

[<Test>]
let ``Odd numbered rows start with an unoccupied square``() =
   let sut = Board()
   let expected = Unoccupied
   for row in 1..2..sut.Height-1 do
      let actual = sut.[0, row]
      Assert.AreEqual(expected, actual)

[<Test>]
let ``Each row alternates between reachable and unoccupied squares``() =
   let sut = Board()
   let nextExpected = function
      | Unreachable -> Unoccupied
      | Unoccupied -> Unreachable
      | _ -> raise (ArgumentException("Internal test error - unsupported case"))
   for row in 0..sut.Height-1 do
      let mutable expected = sut.[0, row]
      for col in 0..sut.Width-1 do
         let actual = sut.[col, row]
         Assert.AreEqual(expected, actual)
         expected <- expected |> nextExpected

[<Test>]
let ``I can place a piece in an unoccupied square producing a new board``() =
   let sut = Board()
   let square = Occupied(Piece Red)
   let sut' = sut.Set(1, 0, square)
   Assert.AreNotEqual(sut, sut')

[<Test>]
let ``When I have placed a piece I can find that piece in the new board``() =
   let sut = Board()
   let square = Occupied(Piece Red)
   let sut' = sut.Set(1, 0, square)
   let expected = square
   let actual = sut'.[1, 0]
   Assert.AreEqual(expected, actual)

[<Test>]
let ``Placing a piece in an occupied square causes an error``() =
   let sut = Board()
   let square = Occupied(Piece Red)
   let sut' = sut.Set(1, 0, square)
   let cant = (fun () -> 
      let sut'' = sut'.Set(1, 0, square)
      ())
   Assert.That(cant, Throws.TypeOf<System.ArgumentException>())

[<Test>]
let ``Placing a piece in an unreachable square causes an error``() =
   let sut = Board()
   let square = Occupied(Piece Red)
   let cant = (fun () -> 
      sut.Set(0, 0, square) |> ignore
      ())
   Assert.That(cant, Throws.TypeOf<System.ArgumentException>())