# Import the packages
import numpy as np
from scipy import stats
from scipy.stats import mannwhitneyu
from numpy.random import seed
from numpy.random import randn
from matplotlib import pyplot
from scipy.stats import kstest

# A group of functions that can be used for data anlysis.
def do_t_test(list1, list2):
    # Define 2 random distributions
    # Sample Size
    # N = 10

    N = len(list1)
    # Gaussian distributed data with mean = 2 and var = 1
    a = list1
    # Gaussian distributed data with with mean = 0 and var = 1
    b = list2

    # Calculate the Standard Deviation
    # Calculate the variance to get the standard deviation

    # For unbiased max likelihood estimate we have to divide the var by N-1, and therefore the parameter ddof = 1
    var_a = a.var(ddof=1)
    var_b = b.var(ddof=1)

    # std deviation
    s = np.sqrt((var_a + var_b) / 2)
    s

    # Calculate the t-statistics
    t = (a.mean() - b.mean()) / (s * np.sqrt(2 / N))

    # Compare with the critical t-value
    # Degrees of freedom
    df = 2 * N - 2

    # p-value after comparison with the t
    p = 1 - stats.t.cdf(t, df=df)

    print("t = " + str(t))
    print("p = " + str(2 * p))

    # Cross Checking with the internal scipy function
    t2, p2 = stats.ttest_ind(a, b)
    print("t = " + str(t2))
    print("p = " + str(p2))


def do_ks_test(list):
    x = kstest(list, "norm")
    print(x)

def do_man_whitney_u_test(list1,list2):
    stat, p = mannwhitneyu(list1, list2)
    print('Statistics=%.3f, p=%.3f' % (stat, p))

if __name__ == '__main__':
    N = 10
    list1 = np.random.randn(N) + 2
    list2 = np.random.randn(N)

    pyplot.hist(list1)
    pyplot.show()
    do_t_test(list1, list2)

    do_ks_test(list1)

    do_man_whitney_u_test(list1,list2)
