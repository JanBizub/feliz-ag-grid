[<RequireQualifiedAccess>]
module HomePageView
open Feliz
open AppTypes

[<ReactComponent>]
let BtnCellRenderer props =
  Html.button [
    prop.classes [| "btn"; "btn-primary"|]
    prop.text "button text"
  ]

[<ReactComponent>]
let RenderAgGrid (state: AppState) dispatch =
  Html.h1 "Homepage"

