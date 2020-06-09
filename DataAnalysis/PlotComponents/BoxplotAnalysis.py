import csv
from pathlib import Path
from os import listdir
from os.path import isfile, join
import pandas as pd
import os
import matplotlib.pyplot as plt

# This is the script for plotting the Box Whisker plot
# ensure your device has this folder and it contains the csv files for Reading first group and VR first group.
jsonFolderPath = './BoxPlotAnalysis'
path = Path(jsonFolderPath).absolute()


def readFile(filename):
    dp = path.joinpath(filename)
    data = pd.read_csv(dp)
    return data


def write_counter(filename, df):
    dp = path.joinpath(filename)
    dflist = df.values.tolist()
    counter_list = []
    for items in dflist:
        counter = 0
        index = 0
        for item in items:
            if item is True:
                counter = counter + 1
        counter_list.append(counter)

    df['Counter'] = counter_list
    print(df)
    df.to_csv(dp)


def plot_box_whisker(df1, df2):
    df = pd.DataFrame()
    df['ReadingFirst'] = df1['Counter'].values.tolist()
    df['VR First'] = df2['Counter']
    boxplot = df.boxplot(column=['VR First', 'ReadingFirst'])
    plt.savefig(jsonFolderPath + '/VR_DATA_PLOT' + '.png')  # generated plots will be saved inside the images folder
    # plt.show()
    return


def main():
    onlyfiles = [f for f in listdir(path) if isfile(join(path, f))]

    df1 = readFile(onlyfiles[0])
    df2 = readFile(onlyfiles[1])

    plot_box_whisker(df1, df2)

    # write_counter(onlyfiles[0], df1)
    # write_counter(onlyfiles[1], df2)
    return


if __name__ == '__main__':
    main()
