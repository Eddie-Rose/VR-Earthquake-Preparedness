import csv
from pathlib import Path
from os import listdir
from os.path import isfile, join
import pandas as pd
import os

# Combine the converted csv file to one csv file
# ensure the folder exists and contains the converted csv files in it.
jsonFolderPath = './UserCSV'
path = Path(jsonFolderPath).absolute()


def readFile(filename, finalDf):
    dp = path.joinpath(filename)
    data = pd.read_csv(dp)

    count = 0
    for index, row in data.iterrows():
        if row['correctness'] is True:
            count = count + 1

    return data


def main():
    directory = 'CombinedCSV'
    if not os.path.exists(directory):
        os.makedirs(directory)

    f = csv.writer(open('./' + directory + '/' + 'combined' + ".csv", "w"))
    f.writerow(
        ["UserName", "O_S0_Shelf", "O_S0_PictureFrame", "O_S0_OfficeChair", "O_S0_CoatStand", "O_S0_Desk",
         "O_S0_Monitor",
         "O_S0_Printer", "O_S1_OfficeChair", "O_S1_Printer", "L_S0_Shelf", "L_S0_Shelf1", "L_S1_RoundBase1",
         "L_S1_RoundVase2",
         "L_S1_Vase", "L_S2_Orchid", "L_S2_CornPlant", "L_S3_StackedBooks", "L_S3_StackedBooks1", "H_S0_Cup",
         "H_S0_CupSmall", "H_S0_SmallestCup",
         "H_S1_TallDrawer", "H_S1_Drawer", "H_S2_HospitalBed", "H_S2_ECGMonitor", "H_S2_IVStand"])
    onlyfiles = [f for f in listdir(path) if isfile(join(path, f))]
    finalDf = pd.DataFrame()
    for file in onlyfiles:
        df = readFile(file, finalDf)
        user = df['user'].values.tolist()
        df1 = df['correctness'].values.tolist()
        list = []
        list.append(user[0])
        list.extend(df1)
        f.writerow(list)
    return


if __name__ == '__main__':
    main()
