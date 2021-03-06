﻿module App.List

let traverseAsyncA f list = 
    let (<*>) = Async.apply
    let retn = Async.retn

    let cons head tail = head :: tail
    let initState = retn []

    let folder head tail = 
        retn cons <*> (f head) <*> tail

    List.foldBack folder list initState

let sequenceAsyncA x = traverseAsyncA id x

let traverseAsyncM f list =
    let (>>=) x f = Async.bind f x
    let retn = Async.retn

    let cons head tail = head :: tail

    let initState = retn []
    let folder head tail = 
        f head >>= (fun h -> 
        tail >>= (fun t ->
        retn (cons h t) ))

    List.foldBack folder list initState

let sequenceAsyncM x = traverseAsyncM id x

let traverseResultA f list = 
    let (<*>) = Result.apply
    let retn = Result.Success

    let cons head tail = head :: tail
    let initState = retn []

    let folder head tail = 
        retn cons <*> (f head) <*> tail

    List.foldBack folder list initState

let sequenceResultA x = traverseResultA id x

let traverseResultM f list =
    let (>>=) x f = Result.bind f x
    let retn = Result.Success

    let cons head tail = head :: tail

    let initState = retn []
    let folder head tail = 
        f head >>= (fun h -> 
        tail >>= (fun t ->
        retn (cons h t) ))

    List.foldBack folder list initState

let sequenceResultM x = traverseResultM id x

let traverseAsyncResultM f list =
    let (>>=) x f = AsyncResult.bind f x
    let retn = AsyncResult.retn

    let cons head tail = head :: tail

    let initState = retn []
    let folder head tail = 
        f head >>= (fun h -> 
        tail >>= (fun t ->
        retn (cons h t) ))

    List.foldBack folder list initState 

let sequenceAsyncResultM x = traverseAsyncResultM id x
