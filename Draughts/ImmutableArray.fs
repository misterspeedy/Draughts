module ImmutableArray

// Based on: http://fpish.net/topic/None/60006
type ImmutableArray2D<'T>(data) =
   new(width, height, init:'T) = ImmutableArray2D<'T>(Array2D.create width height init)
 
   member this.Set(x, y, value) =
      let data' = Array2D.copy data
      data'.[x, y] <- value
      new ImmutableArray2D<'T>(data')
 
   member this.Item
      with get(x, y) = data.[x, y]

   member this.Width = data |> Array2D.length1

   member this.Height = data |> Array2D.length2