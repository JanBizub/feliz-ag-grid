[<RequireQualifiedAccess>]
module AppView
open System
open Feliz
open AppTypes
open Feliz.AgGrid

[<ReactComponent>]
let RenderAgGrid (state: AppState) dispatch =
  Html.div [
      prop.classes [ ThemeClass.Balham; "ag-grid-container" ]
      prop.children [
        AgGrid.grid [
          AgGrid.reactUi true
          AgGrid.immutableData true
          AgGrid.modules [| clientSideRowModelModule |]
          AgGrid.rowData state.GridData
          AgGrid.columnDefs [
            ColumnDef.create<Guid> [
              ColumnDef.headerName "ID"
              ColumnDef.valueGetter (fun d -> d.Id)
            ]

            ColumnDef.create<string> [
              ColumnDef.headerName "Name"
              ColumnDef.editable (fun _ -> true)
              ColumnDef.valueGetter (fun d -> d.Name)
            ]

            ColumnDef.create<int> [
              ColumnDef.headerName "Ammount"
              ColumnDef.valueGetter (fun d -> d.Ammount)
            ]
  
            ColumnDef.create<int option> [
              ColumnDef.headerName "AmmoInMagazine"
              ColumnDef.valueGetter (fun d -> d.AmmoInMagazine)
            ]

            //ColumnDef.create<decimal> [
            //  ColumnDef.headerName "Caliber"
            //  ColumnDef.valueGetter (fun d -> d.Caliber)
            //]

            //ColumnDef.create<DateTime> [
            //  ColumnDef.headerName "DateServiced"
            //  ColumnDef.valueGetter (fun d -> d.DateServiced)
            //]
          ]
        ]
      ]
    ]

[<ReactComponent>]
let Render (state: AppState) dispatch =
  Html.div [
    prop.classes  [ "box-row"; "content" ]
    prop.children [ (state, dispatch) ||> RenderAgGrid ]
  ]
