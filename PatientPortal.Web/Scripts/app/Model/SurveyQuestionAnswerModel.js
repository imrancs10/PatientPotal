//************************************** Survey Question AnswerModel : knockout model **************************************//
define([''], function () {
    function SurveyQuestionAnswerModel() {
        var self = this;
        self.Id = ko.observable();
        self.SurveyId = ko.observable();
        self.QuestionId = ko.observable();
        self.AnswerId = ko.observable();
        self.TextInput = ko.observable();
        self.Guid = ko.observable();
        self.UserId = ko.observable();

        //set data to observable property
        self.SetData = function (model) {
            self.Id(model.Id);
            self.SurveyId(model.SurveyId);
            self.QuestionId(model.QuestionId);
            self.AnswerId(model.AnswerId);
            self.TextInput(model.TextInput);
            self.Guid(model.Guid);
            self.UserId(model.UserId);
        };

        //set data to observable property
        self.SetDataSurveyAttempt = function (model,surveyId) {
            self.SurveyId(surveyId);
            self.QuestionId(model.key);
            self.AnswerId(model.value);
            self.TextInput(model.answerValue);
        };

    }
    return SurveyQuestionAnswerModel;
});
    
