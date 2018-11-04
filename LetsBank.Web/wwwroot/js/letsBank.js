var letsBankNamespace = function () {

    var inputDiv = $('#inputDiv');
    var enterAmountInput = $('#enterAmount');

    var feedbackDiv = $('#feedbackDiv');
    var feedback = $('#feedback');

    var outputDiv = $('#outputDiv');
    var accountBalanceInput = $('#accountBalance');

    var historyDiv = $('#historyDiv');
    var historyTable = $('#historyTable');

    var bankOpsSpinnerDiv = $('#bankOpsSpinnerContainer');

    var depositIt = $('#depositBtn');
    var withdrawIt = $('#withdrawalBtn');

    var getBalance = $('#balanceBtn');
    var getHistory = $('#historyBtn');

    var bankOpsClear = $('#clearBtn');
    var bankOpsBtns = $('.bankOpsBtn');



    var DEBUG = false;

    var viewModel = {

        balance: "0",
        txnRecords: null
    };

    function emptyFeedback() {

        $(feedback).text("");
    }
    function emptyHistory() {

        $(historyTable).empty()
        viewModel.txnRecords = null;
    }

    function clearResults() {

        emptyFeedback();

        $(enterAmountInput).val("");

        $(outputDiv).addClass('hiddenNone');
        $(historyDiv).addClass('hiddenNone');

        emptyHistory();
    }

    function timedClearFeedback() {

        setTimeout(emptyFeedback, 3000);
    }

    function setLoadingState() {

        $(bankOpsSpinnerDiv).removeClass('hiddenNone');
        $(bankOpsBtns).attr('disabled', 'disabled');

        $(outputDiv).addClass('hiddenNone');
        $(historyDiv).addClass('hiddenNone');
    }
    function exitLoadingState() {

        $(bankOpsSpinnerDiv).addClass('hiddenNone');
        $(bankOpsBtns).removeAttr('disabled');

        $(outputDiv).removeClass('hiddenNone');
        $(historyDiv).removeClass('hiddenNone');
    }

    function setupEventListeners() {

        $(depositIt).on("click", depositAmount);
        $(withdrawIt).on("click", withdrawAmount);

        $(getBalance).on("click", getAccountBalance);
        $(getHistory).on("click", getTransactionHistory);

        $(bankOpsClear).on("click", clearResults);
    }

    function getAccountBalance() {

        DEBUG && console.debug("@ getAccountBalance()");

        var startTime = new Date();

        setLoadingState();

        emptyHistory();

        $.ajax({

            url: GetAccountBalanceEndpoint,
            type: 'GET',

            cache: false,

            success: function (ajaxd_accountBalanceData) {

                DEBUG && console.log("Success fetching Account Balance Results");
                DEBUG && console.log(ajaxd_accountBalanceData); // expected type string of decimal 'AccountBalance' - in json object-form

                viewModel.balance = ajaxd_accountBalanceData.balance;
                clearResults();

                $(accountBalanceInput).val(viewModel.balance);

                DEBUG && console.log("Success fetching + loading  Account Balance Results");
            },

            error: function (req, status, error) {

                DEBUG && console.log("Error while loading Account Balance Results");

                DEBUG && console.log("REQUEST:");
                DEBUG && console.log(req.responseText);
                DEBUG && console.log("STATUS:" + status);
                DEBUG && console.log("ERROR:" + error);

                $(feedback).text("An error while loading  Account Balance Results");

                timedClearFeedback();

                // Redirect to Login - to handle - Potential Logged-in cached state from previous app run, that results in HTTP 500 response(this can happen due to the nature of our in-memory Bank Account data store!)
                window.location.replace('/Identity/Account/Login');
            },

            complete: function () {

                exitLoadingState();

                var endTime = new Date();
                var elapsedTimeSecs = (endTime - startTime) / 1000;

                DEBUG && console.debug("@ getAccountBalance() - complete: Elapsed Time = " + elapsedTimeSecs + " seconds");
            }
        });
    }

    function addHistoryTableHeader() {

        var txnHdrRow = '<tr id="txnHeaderRow">' +

            '<th id="txnTypeHdr"        class="txnType          hdr">Transaction Type   </th>' +
            '<th id="txnAmountHdr"      class="txnAmount        hdr">Amount ($)         </th>' +
            '<th id="txnInitBalanceHdr" class="txnInitBalance   hdr">Initial Balance ($)</th>' +
            '<th id="txnFinalBalanceHdr"class="txnFinalBalance  hdr">Final Balance   ($)</th>' +
            '<th id="txnDateHdr"        class="txnDate          hdr">Date               </th>' +

            '</tr >';

        $(historyTable).append(txnHdrRow);
    }
    function getHistoryTableRow(txn) {

        var newRow = '<tr class="txnRecordRow">' +

                '<td class="txnType"        >' + txn.type           + '</td>' +
                '<td class="txnAmount"      >' + txn.amount         + '</td>' +
                '<td class="txnInitBalance" >' + txn.initialBalance + '</td>' +
                '<td class="txnFinalBalance">' + txn.finalBalance   + '</td>' +
                '<td class="txnDate"        >' + new Date(txn.date).toLocaleString(); + '</td>' +

            '</tr>';

        return newRow;
    }

    function updateHistoryDisplay() {

        var txns = viewModel.txnRecords;

        /* Load Transactions to UI display */

        addHistoryTableHeader();

        for (var i = 0; i < txns.length; i++) {

            var txn = txns && txns[i];

            if (typeof  txn !== "undefined" &&
                        txn !== null) {

                var txnRow = getHistoryTableRow(txn);

                $(historyTable).append(txnRow);
            }
        }

        $(historyDiv).removeClass('hiddenNone');
    }
    function getTransactionHistory() {

        DEBUG && console.debug("@ getTransactionHistory()");

        var startTime = new Date();

        setLoadingState();

        emptyHistory();

        $.ajax({

            url: GetTransactionHistoryEndpoint,
            type: 'GET',

            cache: false,

            success: function (ajaxd_transactionHistoryData) {

                DEBUG && console.log("Success fetching Transaction History Results");
                DEBUG && console.log(ajaxd_transactionHistoryData); // expected type string of decimal 'AccountBalance' - in json object-form

                clearResults();
                viewModel.txnRecords = ajaxd_transactionHistoryData.txns;

                updateHistoryDisplay();

                DEBUG && console.log("Success fetching + loading  Transaction History Results");
            },

            error: function (req, status, error) {

                DEBUG && console.log("Error while loading Transaction History Results");

                DEBUG && console.log("REQUEST:");
                DEBUG && console.log(req.responseText);
                DEBUG && console.log("STATUS:" + status);
                DEBUG && console.log("ERROR:" + error);

                $(feedback).text("An error while loading  Transaction History Results");

                timedClearFeedback();
            },

            complete: function () {

                exitLoadingState();

                var endTime = new Date();
                var elapsedTimeSecs = (endTime - startTime) / 1000;

                DEBUG && console.debug("@ getTransactionHistory() - complete: Elapsed Time = " + elapsedTimeSecs + " seconds");
            }
        });
    }

    function withdrawAmount() {

        DEBUG && console.debug("@ withdrawAmount()");

        var startTime = new Date();
        var _amountString = String( $(enterAmountInput).val() );

        setLoadingState();

        $.ajax({

            url: WithdrawEndpoint,
            type: 'POST',

            data: { "amount": _amountString },
            cache: false,

            dataType: "json",

            success: function (withdrawResult) {

                DEBUG && console.log(withdrawResult); // expected type is bool

                if (withdrawResult.result) {

                    DEBUG && console.log("Successful Withdrawal");

                    getAccountBalance();
                }
                else {

                    DEBUG && console.log("Error processing Withdrawal");
                    $(feedback).text("Error processing Withdrawal / Invalid Amount");

                    timedClearFeedback();
                }
            },

            error: function (req, status, error) {

                DEBUG && console.log("Error while processing Withdrawal");

                DEBUG && console.log("REQUEST:");
                DEBUG && console.log(req.responseText);
                DEBUG && console.log("STATUS:" + status);
                DEBUG && console.log("ERROR:" + error);

                $(feedback).text("An error occurred while processing Withdrawal");

                timedClearFeedback();
            },

            complete: function () {

                exitLoadingState();

                var endTime = new Date();
                var elapsedTimeSecs = (endTime - startTime) / 1000;

                DEBUG && console.debug("@ withdrawAmount() - complete: Elapsed Time = " + elapsedTimeSecs + " seconds");
            }
        });
    }
    function depositAmount() {

        DEBUG && console.debug("@ depositAmount()");

        var startTime = new Date();
        var _amountString = String($(enterAmountInput).val());

        setLoadingState();

        $.ajax({

            url: DepositEndpoint,
            type: 'POST',

            data: { "amount": _amountString },
            cache: false,

            dataType: "json",

            success: function (depositResult) {

                DEBUG && console.log(depositResult); // expected type is bool

                if (depositResult.result) {

                    DEBUG && console.log("Successful Deposit");

                    getAccountBalance();
                }
                else {

                    DEBUG && console.log("Error processing Deposit");
                    $(feedback).text("Error processing Deposit /  Invalid Amount");

                    timedClearFeedback();
                }
            },

            error: function (req, status, error) {

                DEBUG && console.log("Error while processing Deposit");

                DEBUG && console.log("REQUEST:");
                DEBUG && console.log(req.responseText);
                DEBUG && console.log("STATUS:" + status);
                DEBUG && console.log("ERROR:" + error);

                $(feedback).text("An error occurred while processing Deposit");

                timedClearFeedback();
            },

            complete: function () {

                exitLoadingState();

                var endTime = new Date();
                var elapsedTimeSecs = (endTime - startTime) / 1000;

                DEBUG && console.debug("@ depositAmount() - complete: Elapsed Time = " + elapsedTimeSecs + " seconds");
            }
        });
    }

    function init() {

        setupEventListeners();
        getAccountBalance();
    }

    return {
        init: init
    };
}

$(document).ready(function () {

    var letsBank = new letsBankNamespace();
        letsBank.init();
});