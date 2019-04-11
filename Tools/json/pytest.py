import xlrd
 
workbook = xlrd.open_workbook('Activity.xlsx')
for booksheet in workbook.sheets():
    for col in range(booksheet.ncols):
        for row in range(booksheet.nrows):
            value = booksheet.cell(row, col).value
            print (value)