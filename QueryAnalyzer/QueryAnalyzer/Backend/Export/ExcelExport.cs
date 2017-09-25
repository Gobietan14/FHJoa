using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using QueryAnalyzer.Utilities;
using System;
using System.IO;
using QueryAnalyzer.Models;
using QueryAnalyzer.Backend.Export;
using System.Collections.Generic;

namespace QueryAnalyzer.Backend
{
    public class ExcelExport : IExport
    {
        private string ColumnLetter(int colNum)
        {
            var intfirstLetter = ((colNum) / 676) + 64;
            var intsecondLetter = ((colNum % 676) / 26) + 64;
            var intthirdLetter = (colNum % 26) + 65;
            var firstLetter = (intfirstLetter > 64) ? (char)intfirstLetter : ' ';
            var secondLetter = (intsecondLetter > 64) ? (char)intsecondLetter : ' ';
            var thirdLetter = (char)intthirdLetter;
            return $"{firstLetter}{secondLetter}{thirdLetter}".Trim();
        }

        private Cell CreateCell(string header, UInt32 index, string text)
        {
            var cell = new Cell
            {
                DataType = CellValues.InlineString,
                CellReference = header + index
            };
            var inline = new InlineString();
            var t = new Text { Text = text };
            inline.AppendChild(t);
            cell.AppendChild(inline);
            return cell;
        }

        private byte[] GenerateDocument(ExcelFile data)
        {
            try
            {
                var stream = new MemoryStream();
                var doc = SpreadsheetDocument.Create(stream, SpreadsheetDocumentType.Workbook);
                var workbookPart = doc.AddWorkbookPart();
                workbookPart.Workbook = new Workbook();
                var worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                var sheetData = new SheetData();

                worksheetPart.Worksheet = new Worksheet(sheetData);
                var sheets = doc.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());

                var sheet = new Sheet()
                {
                    Id = doc.WorkbookPart.GetIdOfPart(worksheetPart),
                    SheetId = 1,
                    Name = data.DocumentName ?? "Sheet 1"
                };
                sheets.AppendChild(sheet);

                UInt32 rowIndex = 0;
                var row = new Row { RowIndex = ++rowIndex };
                sheetData.AppendChild(row);
                var cellIndex = 0;

                foreach (var header in data.Headers)
                {
                    row.AppendChild(CreateCell(ColumnLetter(cellIndex++), rowIndex, header ?? string.Empty));
                }
                if (data.Headers.Count > 0)
                {
                    if (data.ColumnConfiguration != null)
                    {
                        var cols = (Columns)data.ColumnConfiguration.Clone();
                        worksheetPart.Worksheet.InsertAfter(cols, worksheetPart.Worksheet.SheetFormatProperties);
                    }
                }
                foreach (var rowData in data.DataRows)
                {
                    cellIndex = 0;
                    row = new Row { RowIndex = ++rowIndex };
                    sheetData.AppendChild(row);
                    foreach (var cellData in rowData)
                    {
                        var c = CreateCell(ColumnLetter(cellIndex++), rowIndex, cellData ?? string.Empty);
                        row.AppendChild(c);
                    }
                }

                workbookPart.Workbook.Save();
                doc.Close();
                var result = stream.ToArray();
                return result;
            }catch(Exception ex)
            {
                return null;
            }
        }

        public  byte[] Generate(Project data)
        {
            try
            {
                var excel = new ExcelFile();
                excel.Headers = new List<string>() {  "Name", "Description" };
                foreach (var issue in data.Issues)
                {
                    excel.DataRows.Add(new List<string>()
                {
                     issue.Name, issue.Description
                });
                }
                excel.DocumentName = "Test";
                return GenerateDocument(excel);
            }
            catch(Exception exc)
            {
                return null;
            }
        }


    }
}
