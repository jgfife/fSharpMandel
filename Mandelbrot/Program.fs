// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.
open System.IO
let filePath = "C:/Users/fifec/Source/Repos/Mandelbrot/fractal.ppm"
let xcenter = -0.743643135
let ycenter = 0.131825963
let mag = 91152.35
let xsize = 800
let ysize = 600
let iters = 1000

let generateImage =
    let s = sprintf "P3\n%d %d\n255\n" xsize ysize
    File.WriteAllText(filePath, s)
    File.AppendAllText(filePath, "255 245 255 ")
    File.AppendAllText(filePath, "235 245 225")

[<EntryPoint>]
let main argv = 
    printfn "%A" argv
    generateImage
    0 // return an integer exit code
