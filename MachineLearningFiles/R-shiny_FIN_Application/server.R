library(e1071)
library("party")
library("rpart")
library("Metrics")
library(nnet)
library(caret)
server <- function(input, output, session) {
  
  myTestset <- data.frame()
  makeReactiveBinding("myTestset")
  myTrainset <- data.frame()
  makeReactiveBinding("myTrainset")
  myactualVpred <- data.frame()
  makeReactiveBinding("myactualVpred")
  myACCU <- 0
  makeReactiveBinding("myACCU")
  myPrediction <- 0
  makeReactiveBinding("myPrediction")
  myData <- read.csv("data/mydata.csv")

  myVars <- reactive({
    vars_df <- input$indepvar
    vars_df
  })
  
  observe({
    updateSelectInput(session, "indepvar",
                      choices = colnames(myData))
    updateSelectInput(session, "target",
                      choices = levels(myData$target))
    updateSelectInput(session, "needorwant",
                      choices = levels(myData$needorwant))
    updateSelectInput(session, "category",
                      choices = levels(myData$category))
    updateSelectInput(session, "desire",
                      choices = levels(myData$desire))
    
  })

 
  #################################################################################################################################
  output$modelling <- renderPrint({
    mymodel = input$model_radio
    inputdata <- data.frame()
    dataset <- data.frame()
    switch(mymodel, 
           "GLM"= {
        dataset <- data.frame(myData$target, myData$needorwant, myData$category, as.numeric(myData$monthlyincome))
        colnames(dataset) = c("target", "needorwant", "category", "monthlyincome")
        inputdata <- data.frame(input$target, input$needorwant, input$category, as.numeric(input$monthlyincome))
        colnames(inputdata) = c("target", "needorwant", "category", "monthlyincome")
        
           
        },
           
           "SVM"= {
             dataset <- data.frame(myData$target, as.numeric(myData$price))
             colnames(dataset) = c("target", "price")
             inputdata <- data.frame(input$target, as.numeric(input$price))
             colnames(inputdata) = c("target", "price")
           },
           "Decision Tree"= {
             dataset <- data.frame(myData$target, as.numeric(myData$price), as.numeric(myData$monthlyincome),  as.numeric(myData$age))
             colnames(dataset) = c("target", "price", "monthlyincome", "age")
             inputdata <- data.frame(input$target, as.numeric(input$price), input$category, as.numeric(input$monthlyincome), as.numeric(input$age))
             colnames(inputdata) = c("target", "price", "category", "monthlyincome", "age")
             print(head(dataset))
             
             
           },
           "Naive Bayes"= {
             dataset <- data.frame(myData$target, myData$desire, as.numeric(myData$monthlyneeds), as.numeric(myData$monthlyincome), myData$needorwant)
             colnames(dataset) = c("target", "desire","monthlyneeds", "monthlyincome", "needorwant")
             inputdata = data.frame(input$target, input$desire, as.numeric(input$monthlyneeds), as.numeric(input$monthlyincome),input$needorwant)
             colnames(inputdata) =  c("target", "desire","monthlyneeds", "monthlyincome", "needorwant")
             
           }
    
    )
    
 
    #splitting into training and test sets
    n=nrow(dataset)
    set.seed(175)
    indexes = sample(n,n*(50/100))
    trainset = dataset[indexes,]
    testset = dataset[-indexes,]
    myTrainset <<- trainset
    myTestset <<- testset
    target <- myData$target
    actual <- testset$target
    
    switch(mymodel, 
           "GLM"= {
             print("GLM")
             lr_model<- multinom(target ~ .,data=trainset, family="binomial")
             lr_pred = predict(lr_model,testset)
             myACCU <<- mean(lr_pred == actual)
             myPrediction <<- predict(lr_model, inputdata)
             myactualVpred <<- data.frame(actual, lr_pred)
             print(lr_model)
             confusion_matrix <- confusionMatrix(lr_pred, actual)
             print(confusion_matrix)
             
           },
           
           "SVM"= {
             print("SVM here")
             svr_model <- svm(target ~ ., data=trainset, method="C-classification")
             svr_pred = predict(svr_model, testset)
             myACCU <<- mean(svr_pred == actual)
             myPrediction <<- predict(svr_model, inputdata)
             myactualVpred <<- data.frame(actual, svr_pred)
             print(svr_model)
             confusion_matrix <- confusionMatrix(svr_pred, actual)
             print(confusion_matrix)
             
           },
           "Decision Tree"= {
             print("Decision Tree")
           #  print(dataset)
             rpart_model <- rpart(target~., method="class", data=trainset)
             rpart_pred <- predict(rpart_model, testset, type='class')
             myACCU <<- mean(rpart_pred == actual)
             myPrediction <<- predict(rpart_model, inputdata, type='class')
              myactualVpred <<- data.frame(actual, rpart_pred)
             print(rpart_model)
             confusion_matrix <- confusionMatrix(rpart_pred, actual)
              print(confusion_matrix)
             
           },
           "Naive Bayes"= {
             print("Naive Bayes")
             nb_model <- naiveBayes(target ~ ., data = trainset)
             nb_pred = predict(nb_model, testset) 
             myACCU <<- mean(nb_pred == actual)
             myPrediction <<- predict(nb_model,inputdata)
             myactualVpred <<-data.frame(actual, nb_pred)
             print(nb_model)
             confusion_matrix <- confusionMatrix(nb_pred, actual)
             print(confusion_matrix)
           },
           {
             print('Please choose a model.')
           }
    )

  })
  #################################################################################################################################
  #################################################################################################################################

  # Data output
  output$datatable = DT::renderDataTable({
    
        df <- myData
        df

  })

  output$testset = DT::renderDataTable({
    myTestset
  })
  output$trainset = DT::renderDataTable({
    myTrainset
  })
  
  output$actualVpred = DT::renderDataTable({
    colnames(myactualVpred) = c("actual", "prediction")
    myactualVpred
  })
  
  output$PLOTactualVpred = renderPlot({
    colnames(myactualVpred) = c("actual", "prediction")
    plot(myactualVpred)
  })
  
  output$accu <- renderValueBox({
    valueBox(
      myACCU, "Accuracy", icon = icon("check"),
      color = "purple"
    )
  })
  
  output$prediction <- renderValueBox({
    valueBox(
      myPrediction, "Recommendation", icon = icon("lightbulb"),
      color = "yellow"
    )
  })

}