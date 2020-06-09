from os import listdir
from os.path import isfile, join
from pathlib import Path

import pandas as pd

# A script for counting the number of correct answers from the csv files.
csvFolderPath = './UserQuestionnaire'
path = Path(csvFolderPath).absolute()


def readFile(filename):
    dp = path.joinpath(filename)
    df = pd.read_csv(dp)
    dflist = df.values.tolist()
    q1counts = []
    q2counts = []
    q3counts = []
    for items in dflist:
        count1 = 0
        count2 = 0
        count3 = 0
        index = 0
        for item in items:
            if item is True:
                if index <= 5:
                    count1 = count1 + 1
                elif index <= 10:
                    count2 = count2 + 1
                elif index <= 15:
                    count3 = count3 + 1
            index = index + 1

        q1counts.append(count1)
        q2counts.append(count2)
        q3counts.append(count3)

    df['Qustionnaire1'] = q1counts
    df['Qustionnaire2'] = q2counts
    df['Qustionnaire3'] = q3counts

    return df[['User Name', 'Qustionnaire1', 'Qustionnaire2', 'Qustionnaire3']]



def main():
    onlyfiles = [f for f in listdir(path) if isfile(join(path, f))]
    for file in onlyfiles:
        df = readFile(file)
        # plotGraph(df, file)

        df.to_csv('Processed'+file)
    return


if __name__ == '__main__':
    main()
