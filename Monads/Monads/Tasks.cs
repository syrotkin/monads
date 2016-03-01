using System;
using System.Threading;
using System.Threading.Tasks;

namespace Monads {

    // The code is from http://blogs.msdn.com/b/pfxteam/archive/2010/11/21/10094564.aspx
    public static class Tasks {

        public static Task<T2> Then<T1, T2>(this Task<T1> first, Func<T1, Task<T2>> next) {
            if (first == null) {
                throw new ArgumentException("first");
            }
            if (next == null) {
                throw new ArgumentException("next");
            }
            var tcs = new TaskCompletionSource<T2>();
            first.ContinueWith(task => {
                if (first.IsFaulted) {
                    tcs.TrySetException(first.Exception.InnerExceptions);
                } else if (first.IsCanceled) {
                    tcs.TrySetCanceled();
                } else {
                    // success
                    try {
                        var t = next(first.Result);
                        if (t == null) {
                            tcs.TrySetCanceled();
                        } else {
                            t.ContinueWith(t1 => {
                                if (t.IsFaulted) {
                                    tcs.TrySetException(t.Exception.InnerExceptions);
                                } else if (t.IsCanceled) {
                                    tcs.TrySetCanceled();
                                } else {
                                    tcs.TrySetResult(t.Result);
                                }
                            });
                        }
                    } catch (Exception ex) {
                        tcs.TrySetException(ex);
                    }
                }
            }, TaskContinuationOptions.ExecuteSynchronously);
            return tcs.Task;
        }
    }

    /// <summary>
    /// The better way of composing Tasks
    /// </summary>
    public class Awaits {

        public async Task<string> DoAAsync(string input) {
            var task = new Task<string>(() => {
                Thread.Sleep(1000);
                return input + "A";
            });
            var result = await task;
            return result;
        }

        public async Task<string> DoBAsync(string inputA) {
            var task = new Task<string>(() => {
                Thread.Sleep(1000);
                return inputA + "B";
            });
            var result = await task;
            return result;
        }

        public async void Run() {

            var resultA = await DoAAsync("input0");
            var resultB = await DoBAsync(resultA);
        }
    }

}