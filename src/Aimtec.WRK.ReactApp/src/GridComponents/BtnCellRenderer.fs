[<RequireQualifiedAccess>]
module BtnCellRenderer
open System
open Feliz
open Feliz.AgGrid

type BtnCellRendererParams = {
  Text      : string
  Classes   : string array
  OnClick   : Browser.Types.MouseEvent -> unit
  }

[<ReactComponent>]
let Render (props: BtnCellRendererParams) =
  Html.button [
    prop.classes props.Classes
    prop.text    props.Text
    prop.onClick props.OnClick
  ]