[<NUnit.Framework.TestFixture>]
module BoardTests

open System
open NUnit.Framework

[<Test>]
let ``The board is 8x8``() =
   let expected = true
   let actual = false
   Assert.AreEqual(expected, actual)

[<Test>]
let ``I can access a square in the board by column and row``() =
   let expected = true
   let actual = false
   Assert.AreEqual(expected, actual)

[<Test>]
let ``Accessing a square outside the board returns an error``() =
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