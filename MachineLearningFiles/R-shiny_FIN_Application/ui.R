library(shiny)
library(shinydashboard)

ui <- dashboardPage( skin="blue",
  dashboardHeader(title = "Mary Louise Reyes - 10298996", titleWidth = 400),
  dashboardSidebar(
    width = 400,
    sidebarMenu(
      menuItem("Results", tabName = "model", icon = icon("vector-square")),
      menuItem("Data", tabName = "data", icon = icon("table"))
    ),
    selectInput(inputId = "target", label = "Do you want to make the purchase?", choices = "" ),
    textInput("price", "Price"),
    textInput("monthlyincome", "Monthly Income"),
    textInput("age", "Age"),
    textInput("monthlyneeds", "Monthly Budget for needs in %"),
    selectInput(inputId = "needorwant", label = "Need or Want", choices = ""),
    selectInput(inputId = "category", label = "Category",  choices = ""),
    selectInput(inputId = "desire", label = "How much do you want to buy this?", choices = ""),
    radioButtons("model_radio", "Algorithm", choices = list("GLM", "SVM", "Decision Tree", "Naive Bayes"))
  ),
  
  body <- dashboardBody(
    tabItems(
      tabItem(tabName = "model",
              h3("BSc (Hons) in Computing"),
              h3("Final Year Project"),
              fluidRow(
                valueBoxOutput("prediction"),
                valueBoxOutput("accu"),
                box( background = "olive", collapsible = TRUE, title = "Model Summary", verbatimTextOutput("modelling")),
                tabBox(
                  title = "Actual v Pred",
                  height = "350px",
                  selected = "Values",
                  tabPanel("Values", DT::dataTableOutput('actualVpred')),
                  tabPanel("Plot", plotOutput('PLOTactualVpred'))
                ),
                tabBox(
                  title = "Train & Test Sets",
                  height = "350px",
                  selected = "Test Set",
                  tabPanel( "Test Set", DT::dataTableOutput('testset')),
                  tabPanel("Train Set", DT::dataTableOutput('trainset'))
                )
              )
      ),
      
      tabItem(tabName = "data",
              h2("Data"),
              DT::dataTableOutput('datatable')
      )
    )
  )
  
  
  
)

