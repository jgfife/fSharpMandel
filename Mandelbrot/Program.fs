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

let mandel (maxiters : int, x : float, y : float) =
    let mutable a = x
    let mutable b = y
    let mutable result = 0
    for i in 1 .. maxiters do
        let a2 = a * a
        let b2 = b * b
        if a2 + b2 >= 4.0 then result <- i
        let ab = a * b
        b <- ab + ab + y
        a <- a2 - b2 + x
    result

let calcPixel(col : int, row : int, sizeX : int, sizeY : int, centerX : float, centerY : float, magnification : float) =
    let mutable minsize = sizeX
    if sizeY < sizeX then
        minsize <- sizeY
    let x = centerX + float (col - sizeX/2) / (magnification * float (minsize-1))
    let y = centerY - float (row - sizeY/2) / (magnification * float (minsize-1))
    let i = mandel (iters, x, y)
    if i = 0 then
        (0, 0, 0)
    else
        (0, i%256, 0)


let generateImage =
    let s = sprintf "P3\n%d %d\n255\n" xsize ysize 
    File.WriteAllText(filePath, s)
    for row in 0 .. ysize-1 do
        let mutable strings = []
        for col in 0 .. xsize-1 do
            let r, g, b = calcPixel(col, row, xsize, ysize, xcenter, ycenter, mag)
            let rgb = sprintf "%d %d %d" r g b
            strings <- List.append strings [rgb]
        strings <- List.append strings ["\n"]
        File.AppendAllText(filePath, String.concat " " strings)
        printfn "%i" row

[<EntryPoint>]
let main argv = 
    printfn "%A" argv
    generateImage
    0 // return an integer exit code
