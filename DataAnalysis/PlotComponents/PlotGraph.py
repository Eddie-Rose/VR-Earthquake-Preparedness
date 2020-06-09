from pathlib import Path
from os import listdir
from os.path import isfile, join
import pandas as pd
import matplotlib.pyplot as plt

# This script is used for plotting the graphs for the objects in the experiment
# change path to the location of Records folder when you use this on your device
# this folder is the folder that contains the data of the objects for the research.
csvFolderPath = 'E:/Part4Project/PreviousProject/vr-earthquake-lab/VR Earthquake Lab/Records'
path = Path(csvFolderPath).absolute()


def readFile(filename):
    dp = path.joinpath(filename)
    df = pd.read_csv(dp)
    df1 = df.iloc[20:]
    print(df1.head())
    return df1


def plotGraph(df, filename):
    x = df[df.columns[0]]
    y0 = df[df.columns[1]]
    y1 = df[df.columns[2]]
    y2 = df[df.columns[3]]
    y3 = df[df.columns[4]]

    plt.figure(figsize=(20, 8))
    plt.subplot(2, 2, 1)
    plt.plot(x, y0, 'g--')
    plt.title('x-orientation vs time')
    plt.ylabel('object orientation')
    plt.xlabel('time')

    plt.subplot(2, 2, 2)
    plt.plot(x, y1, 'b--')
    plt.title('y-orientation vs time')
    plt.ylabel('object orientation')
    plt.xlabel('time')

    plt.subplot(2, 2, 3)
    plt.plot(x, y2, 'r--')
    plt.title('z-orientation vs time')
    plt.ylabel('object orientation')
    plt.xlabel('time')

    plt.subplot(2, 2, 4)
    plt.plot(x, y3, 'y--')
    plt.title('displacement vs time')
    plt.ylabel('object orientation')
    plt.xlabel('time')

    plt.tight_layout()
    sep = '.'
    name = filename.split(sep, 1)[0]
    plt.savefig('./images/' + name + '.png')  # generated plots will be saved inside the images folder
    # plt.show()
    plt.clf()
    plt.cla()
    plt.close()
    return


def main():
    onlyfiles = [f for f in listdir(path) if isfile(join(path, f))]
    for file in onlyfiles:
        df = readFile(file)
        plotGraph(df, file)

    return


if __name__ == '__main__':
    main()
