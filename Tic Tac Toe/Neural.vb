Imports System.Threading

''' <summary>
''' Houses functions needed for the Neural Network
''' </summary>
Public Class NeuralFunctionHandler

    ''' <summary>
    ''' Returns a random neuron connection strength, between -1 and 1
    ''' </summary>
    ''' <returns>Strength, as double</returns>
    Public Function ReturnRandomStrength() As Double
        Dim random As New Random()
        Return (random.NextDouble * 2) - 1
    End Function

End Class

''' <summary>
''' Class for the overall Neural Network
''' </summary>
Public Class Neural
    'Variables we need to run the network effectively
    Dim topLayerCount As Integer
    Dim midLayerCounts As List(Of Integer)
    Dim outputLayerCount As Integer

    Dim topLayer As New List(Of Neuron)
    Dim midLayers As New List(Of List(Of Neuron))
    Dim bottomLayer As New List(Of Neuron)

    'Reference to the neural function handler
    Dim functions As New NeuralFunctionHandler()

    'Public learning rate so it can be used by the neurons
    Public learningRate As Double = 0.1

    ''' <summary>
    ''' Constant proportion multiplier
    ''' </summary>
    Private kp As Double = 0.1

    ''' <summary>
    ''' Constant integral multiplier
    ''' </summary>
    Private ki As Double = 0.1

    ''' <summary>
    ''' Constant derivative multiplier
    ''' </summary>
    Private kd As Double = 0.1

    ''' <summary>
    ''' PID proportion
    ''' </summary>
    Private proportion As Double

    ''' <summary>
    ''' PID integral
    ''' </summary>
    Private integral As Double

    ''' <summary>
    ''' pid derivative
    ''' </summary>
    Private derivative As Double

    ''' <summary>
    ''' last PID error
    ''' </summary>
    Private lastError As Double

    ''' <summary>
    ''' changes the learning rate of the neural network based on how close it is to being accurate.
    ''' </summary>
    ''' <param name="errorNum">Closeness to the correct number as an integer scaled to 0, where 0 is perfect.</param>
    Public Sub errorPID(errorNum As Integer)
        proportion = errorNum

        integral += proportion
        derivative = proportion - lastError
        lastError = proportion

        learningRate = kp * proportion + ki * integral + kd * derivative
    End Sub

    ''' <summary>
    ''' Create a new neural network with a top layer, (n) middle / hidden layers, and a bottom (output) layer. Uses Thread.Sleep to avoid multiple neurons being references of the same memory address.
    ''' </summary>
    ''' <param name="top">The number of neurons in the top (input) layer of the network (int)</param>
    ''' <param name="mid">A list containing the number of neurons in each layer beneath the top layer of the network (List<int>)</int></param>
    ''' <param name="output">The number of neurons in the output layer of the network.(int)</param>
    Public Sub New(top As Integer, mid As List(Of Integer), output As Integer)
        'iterate through the top layer.
        For i As Integer = 0 To top - 1 Step 1
            topLayer.Add(New Neuron(Me))

            'Thread.Sleep is used because without it, every neuron is a reference to the same neuron. This is an odd bug.
            Thread.Sleep(3)
        Next i

        'Store the last created layer, so we can pass the reference to the next layer.
        Dim lastLayer As List(Of Neuron)
        lastLayer = topLayer

        'Iterate through all the layers in the middle, then iterate through creating every neuron in each layer.
        For i As Integer = 0 To mid.Count - 1 Step 1
            midLayers.Add(New List(Of Neuron))
            For x As Integer = 0 To mid.Item(i) - 1 Step 1
                midLayers.Item(i).Add(New Neuron(Me, lastLayer))
                Thread.Sleep(3)
            Next x
            'maintain reference to the last created layer
            lastLayer = midLayers.Last
        Next i

        'iterate through bottom (output) layer, and pass in the last layer from the middle.
        For i As Integer = 0 To output - 1 Step 1
            bottomLayer.Add(New Neuron(Me, lastLayer))
            Thread.Sleep(3)
        Next i
    End Sub

    ''' <summary>
    ''' Creates a new neural network with only the number of neurons in each layer.
    ''' </summary>
    ''' <param name="top">Neurons in the top layer (int)</param>
    ''' <param name="mid">Neurons in the middle layer (int)</param>
    ''' <param name="output">Neurons in the output layer (int)</param>
    Public Sub New(top As Integer, mid As Integer, output As Integer)
        'iteratively create top (input) layer
        For i As Integer = 0 To top - 1 Step 1
            topLayer.Add(New Neuron(Me))
        Next i

        'create a reference to the above layer
        Dim lastLayer As List(Of Neuron)
        lastLayer = topLayer

        'iterate through the middle layer, creating neurons, and linking back to the input layer
        midLayers.Add(New List(Of Neuron))
        For x As Integer = 0 To mid - 1 Step 1
            Dim n As New Neuron(Me, lastLayer)
            midLayers.Item(0).Add(n)
        Next x
        lastLayer = midLayers.Last

        'Make the output layer
        For i As Integer = 0 To output - 1 Step 1
            bottomLayer.Add(New Neuron(Me, lastLayer))
        Next i
    End Sub

    ''' <summary>
    ''' Trains the neurons in the neural network.
    ''' </summary>
    ''' <param name="expectedOutputs">The expected outputs of the last data put into the network.</param>
    Public Sub train(expectedOutputs As List(Of Double))
        'This should NEVER happen. If it does, unexpected things will happen, so it's better to throw so that it can be identified.
        If (expectedOutputs.Count <> bottomLayer.Count) Then
            Throw New System.Exception("ERROR! Size mismatch between output layer and training data.")
        End If
        'loop through the output layer, giving the expected outputs, then train them
        For i As Integer = 0 To bottomLayer.Count - 1 Step 1
            bottomLayer.Item(i).setError(expectedOutputs.Item(i))
            bottomLayer.Item(i).train()
        Next
        'loop backwards through the middle layers, training each one off the one below it.
        For j As Integer = midLayers.Count - 1 To 0 Step -1
            For i As Integer = 0 To midLayers.Item(j).Count - 1 Step 1
                midLayers.Item(j).Item(i).train()
            Next i
        Next j
    End Sub

    ''' <summary>
    ''' Call a response from the neural network.
    ''' </summary>
    ''' <param name="inputs">The direct inputs to place into the input layer of the network. Must be the same size as the input layer of the network. As a List of Doubles.</param>
    ''' <returns>Returns a list representing the response, where the higher the number at that index, the more certain the network is.</returns>
    Public Function respond(inputs As List(Of Double)) As List(Of Double)
        'Once again, this should NEVER happen, and if it does, bad things will happen, so it's best to break.
        If (inputs.Count <> topLayer.Count) Then
            Throw New System.Exception("ERROR! Size mismatch between input layer and data to respond to.")
        End If

        'Loop through the input layer inputting the data
        For i As Integer = 0 To topLayer.Count - 1 Step 1
            topLayer.Item(i).output = inputs.Item(i)
        Next i

        ''loop through the mid layers getting a response from each neuron
        For j As Integer = 0 To midLayers.Count - 1 Step 1
            For h As Integer = 0 To midLayers.Item(j).Count - 1 Step 1
                midLayers.Item(j).Item(h).respond()
            Next h
        Next j

        'make an array to store the outputs, then get responses from the last layer, and put them into the output array.
        Dim outputs As New List(Of Double)
        For i As Integer = 0 To bottomLayer.Count - 1 Step 1
            bottomLayer.Item(i).respond()
            outputs.Add(bottomLayer.Item(i).output)
        Next i

        'return the outputs.
        Return outputs
    End Function
End Class

''' <summary>
''' Class for individual neurons
''' </summary>
Public Class Neuron

    ''' <summary>
    ''' Inputs into this neuron, as a reference list to the neural inputs.
    ''' </summary>
    Dim inputs As New List(Of Neuron)

    ''' <summary>
    ''' The weights to multiply the inputs by. Always the same length as the inputs.
    ''' </summary>
    Dim weights As New List(Of Double)

    ''' <summary>
    ''' The parent neural network. This contains the learning rate for the neural network, so must be available by reference.
    ''' </summary>
    Dim parent As Neural

    ''' <summary>
    ''' One of many things that had to be done to ensure that each neuron had a different pointer. Not used internally, but was used for debugging, and helps to keep each neuron distinct in memory.
    ''' </summary>
    Public id As Double = Rnd()

    ''' <summary>
    ''' The output of this neuron.
    ''' </summary>
    Public output As Double = 0.0

    ''' <summary>
    ''' The error of this neuron. Used in training.
    ''' </summary>
    Dim neuralError As Double = 0.0

    ''' <summary>
    ''' Returns results of sigmoid function, at a specific number. This is the activation strength of each neuron.
    ''' </summary>
    ''' <param name="at">The number to return the sigmoid at. (as a double)</param>
    ''' <returns>Results of the sigmoid function, at the parameter's value.</returns>
    Function Sigmoid(at As Double) As Double
        Return (2.0 / (1.0 + Math.Exp(-2.0 * at))) - 1.0
    End Function

    ''' <summary>
    ''' Creates a neuron without any inputs (this must be the input layer)
    ''' </summary>
    ''' <param name="network">The parent neural network.</param>
    Public Sub New(ByRef network As Neural)
        inputs = New List(Of Neuron)
        weights = New List(Of Double)
        id = Rnd()
        parent = network
    End Sub

    ''' <summary>
    ''' Creates a neuron with a parent and the previous layer passed in. This must not be an input layer neuron.
    ''' </summary>
    ''' <param name="network">The parent neural network.</param>
    ''' <param name="prevLayer">Reference to the layer of neurons above this neuron.</param>
    Public Sub New(ByRef network As Neural, ByRef prevLayer As List(Of Neuron))

        inputs = New List(Of Neuron)
        weights = New List(Of Double)
        id = Rnd()

        'set the weights for each neuron in the above layer, while adding the neurons to the input variable
        For i As Integer = 0 To prevLayer.Count - 1 Step 1
            inputs.Add(prevLayer.Item(i))

            'randomize decreased the proportion of the time that the references would be exactly the same
            Randomize()
            weights.Add(Rnd() * 2 - 1)
        Next i

        'set the parent
        parent = network

    End Sub


    ''' <summary>
    ''' Goes through the above neurons by their output and multiplies by their weight, adding all this together, then generating an output based on this sum. Sets error to 0, because a different result means that this neuron must be trained again with the correct result, which should be different.
    ''' </summary>
    Public Sub respond()
        'Make a variable to store what will become the output strength of the neuron
        Dim inputSum As Double = 0

        'loop through the layer above, multiplying each output by the weight stored
        For i As Integer = 0 To inputs.Count - 1 Step 1
            inputSum += inputs.Item(i).output * weights.Item(i)
        Next

        'set the output to be the result of a sigmoid at the inputsum. This scales the output non-linearly, and provides a good way to avoid a situation where one neuron skews the entire network by growing a large output.
        output = Sigmoid(inputSum)
        neuralError = 0
    End Sub

    ''' <summary>
    ''' Sets the error of this neuron based on the correct output, compared to the output provided
    ''' </summary>
    ''' <param name="correct">The correct response as a double.</param>
    Public Sub setError(correct As Double)
        neuralError = correct - output
    End Sub

    ''' <summary>
    ''' Trains this neuron, by changing the error of above neurons and changing the weights of the above neurons.
    ''' </summary>
    Public Sub train()
        'Delta is the weight multiplier used to edit the weight each neuron puts on each other neuron. 1-output * 1+output is used so that it can't mutiply by 0. Multiply this by the error (which comes from the expected result), and the learning rate (which comes from the network class) so that we can scale the weights based not only on correctness but also the learning rate. If this learning rate is lower, then the neuron is slower to 'learn', but also slower to 'forget'. Also, the delta should logically be lower if the neuron was closer to the correct output, which is why neuralerror contributes to delta. All this has to relate to the actual output, which is why that's included.
        Dim delta As Double = (1 - output) * (1 + output) * neuralError * parent.learningRate

        For i As Integer = 0 To weights.Count - 1 Step 1
            'increase / decrease the above neuron's neural error by the neural error times the weight stored on this neuron (scale the change)
            inputs.Item(i).neuralError += weights.Item(i) * neuralError
            'change the weight of the given input neuron based on the delta times the output of the neuron above. This once again scales neurons so that more correct ones have higher weights.
            weights.Item(i) += inputs.Item(i).output * delta
        Next i

    End Sub

End Class