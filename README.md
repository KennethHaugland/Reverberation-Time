<ul class="download">
	<li><a href="416509/WpfReverberationTime-noexe.zip">Download WpfReverberationTime-noexe.zip - 64 KB</a></li>
	<li><a href="WpfReverberationTime.zip">Download WpfReverberationTime.zip - 403.9 KB</a>&nbsp;</li>
</ul>

<p><img height="604" src="RevTime_Screen.png" width="584" /></p>

<h2>Introduction&nbsp;&nbsp;</h2>

<p>Most of the action related games today usually implement some kind of algorithm for calculation of the sound reflections in either a room or tunnel or other enclosed spaces. There are several ways to implement this modification of the sound that will occur in a room, but I am going to show you a couple of the simplest ways using classical theory to show you how acousticians see a room.</p>

<p>The sound in a room is quite important to simulate, as we make many assumptions about the space we are in by what we hear. For a realistic reproduction of the sound, we need to implement some modifications to the sound pressure, and here the actual acoustics plays a part.&nbsp;</p>

<p>The program that follows this article calculates the reverberation time of a rectangular room, were you can choose the surface elements. &nbsp;&nbsp;&nbsp;</p>

<h2>Reverberation &nbsp;&nbsp;&nbsp;</h2>

<p>I am going to explain reverberation in the following way: If you stand in a valley and scream at a mountain with a reasonably flat surface, you would hear an echo, as the sound will travel from your mouth to the flat wall, and reflect back to your ears. The reason that you don&#39;t hear it in a room is that the reflections from the ceiling floor and walls are so close together that you just assume that the echo is part of the voice. This is also present in instruments as for instance guitar, piano, drums, and so on, as they give off sound long after the tone is initiated even if you stop the vibration in the strings.&nbsp;</p>

<p>The first to consider the reverberation time was the American physicist W. C. Sabine, his article in 1900 outlined the result of the general equation given below (were w is the source energy, V is the Volume, and W<sub>abs</sub> is the absorption in the room):</p>

<p><img height="88" src="Sabines_background_equation.png" width="247" /></p>

<p>As with all equations, it seems rather difficult at first, but this one is actually quite simple. It simply says, the energy from our source (loudspeaker, car voice or anything that makes a sound in the enclosed room) is equal to the momentary energy equally distributed in the room, plus the energy absorbed from our surfaces (the reduced echo from the walls, ceiling or floor). Absorption is actually the procentwise decay of sound from one reflection (similar to <a href="http://en.wikipedia.org/wiki/Snell%27s_law">Snell</a>&#39;s reflection law for light, except absorption describes the loss of energy in the reflected wave), and is usually given as a factor between 0 - 1.0. The most important aspect to contemplate in the formula, is that the sound energy according to Sabine&#39;s equation is equal everywhere in the room. &nbsp;&nbsp;</p>

<p>We will skip a lot of the details in developing the equation to something we can use. In general we assume that all the sound energy that would reach the walls, would take the form of a plan wave,&nbsp; and transforming the general equation with this assumption gives the p<sup>2</sup> value at any given time. The result is this equation below (were rho<sub>0</sub> is the weight of air, in this case (1.21 kg/m<sup>3</sup>), and C<sub>0</sub> is the speed of sound in air (343 m/s) and A = Surface area* Absorption coefficient, and V is the Volume of the room, and t is the time. This applies for all the formulas given in the article):</p>

<p><img height="60" src="Equation_pressure.png" width="249" /></p>

<p>The absorption area in Sabine&#39;s formula is calculated from the equation below (were S is the surface area and alfa is the absorption coefficient for the material):&nbsp;&nbsp;</p>

<p><em>A = S<sub>1</sub> &alpha;<sub>1</sub> + S<sub>2</sub> &alpha;<sub>2</sub> + ... + S<sub>n</sub> &alpha;<sub>n</sub> = &sum; S<sub>i</sub>&alpha;<sub>i</sub> </em></p>

<p>The reverberation time could be extrapolated from this equation. If we set up a source that continually supplies us with a know effect and turn it off, that is set w = 0 at t=0, we can calculate the sound pressure since the time we switch off the source like this:</p>

<p><img height="67" src="Sabine_decay.png" width="224" /></p>

<p>We are however only, as Sabine defined the reverberation time, interested in how long the soundfield takes to decay with 60 dB, and this is actually independent of the sound source. So the formula for reverberation time could the be set like this:</p>

<p><img height="48" src="Sabine_walls.png" width="216" /></p>

<p>Not surprisingly, this differential equation could be solved by using the number e. As a side note in the article this number appears in many solutions, because the number e<sup>x</sup> is its own derivative and integrant, which can be shown by the simple formula below using nonbounded integral from 0 to x (I have not seen it written like this anywhere else though):&nbsp;&nbsp;&nbsp;</p>

<p><img height="73" src="Number_e.png" width="583" /></p>

<p>Shortly after a number of revisions to the theory came, and Eyring-Norris (although Schuster and Waetzmann found the result independently) is based on the notion that a sound wave would have an average distance to travel before it hits a wall. That means that a wave in the form of a ray, would on average travel a distance between the reflections. This could easily be explained. If you place a laser light at any point wall, ceiling, or floor, and shoot many rays in a random direction, the average length before the laser light hits a surface would be the mean distance in the room. In room acoustics we are however more interested in the number of reflections per second, and this could be found by the formula below, were c is the speed of sound, L<sub>n</sub>is the length of x, y, and z direction in a rectangular room, and V is the total volume in the room (the two different formulas are derived using different techniques but they yield the same result):</p>

<p><img height="93" src="reflections_per_second_In_a_room.png" width="362" /></p>

<p>With this information we can easily find the number of reflections per time unit and thus a function for the decay rate of a reverberation field. The equation is developed like the one below (one small remark is that the <em>alpha is the average absorption coefficient in the formula below, and can be formulated as a<sub>alpha</sub> = A / S</em><em>)</em>:&nbsp;</p>

<p><img height="53" src="RevTime_Eyring.png" width="217" /></p>

<p>With the average absorption on the walls calculated like this:</p>

<p><img height="67" src="Average_Abs.png" width="144" /></p>

<p>This is actually quite similar to Sabine&#39;s equation, except that that alpha is a little bigger due to the incident angels instead of the plan wave. This is more clear if we take the series expansion of the absorption function:&nbsp;&nbsp;</p>

<p><img height="55" src="Taylor_Series_alpha.png" width="248" /></p>

<p>But the problem with Eyring-Norris is that the incident angle is diffuse over the entire room, this could be a problem if the absorption coefficient on some of the surfaces is very different on some sections. The is is way Millington-Sette made the modification that you take Eyring-Norris formula and apply it to smaller sections. Millington-Sette gives the following expression:</p>

<p><img height="58" src="RevTime_Millington_WOTair.png" width="242" /></p>

<p>There is a simple formula for the annutation coefficient that is valid between 1.5 to 10 kHz, were h is relative humidity (valid between 20 - 70 %) i percent and f is frequency. The approximate formula is given below:&nbsp;&nbsp;&nbsp;</p>

<p><img height="57" src="humidity.png" width="235" /></p>

<p>The exact formula for the annutation in air is given in ISO 9613-2 and it can be view <a href="http://www.esru.strath.ac.uk/Courseware/Acoustic/aco.htm">here</a>.</p>

<p>They are coupled together with the formulas for reverberation, as air annutation is just another absorption coefficient. The resulting in the modified equations for Sabine:&nbsp;&nbsp;</p>

<p><img height="57" src="Sabine_WallsAndAir.png" width="161" /></p>

<p>Eyring-Norris:&nbsp;&nbsp;</p>

<p><img height="51" src="RevTime_EyringAir.png" width="247" /></p>

<p>Millington-Sette:</p>

<p><img height="60" src="RevTime_Millington.png" width="293" /></p>

<p>The problem with these reverberation formulas is that they are not absolutely correct. You can see why when you see what kind of assumptions that lay behind the development of them. One of the main problems is that you could have standing waves in a room in all the directions, and this leads to a different perceived reverberation time than you would have calculated&nbsp; by any of the three formulas above. One method to try and remedy the situation is the equation by Frizroy, who is based on calculation the reverberation time in the different directions and average them. His proposed a formula looks like this:&nbsp;&nbsp;</p>

<p><img height="53" src="RevTime_Fritzroy.png" width="409" /></p>

<p>An approximate formula with air absorption (if you do the math you&#39;ll see that it is not accurate but its usually not too bad either):&nbsp;</p>

<p><img height="101" src="RevTime_FritzroyAir.png" width="689" /></p>

<p>In recent years there has been several other formulas (which are not given here) that also tries to capture the different reverberation times in the different directions. But unless you take scattering into account you won&#39;t get the correct reverberation time curve. You can see the <a href="http://www.youtube.com/watch?v=uHVFKQpNeR8&amp;feature=plcp">video</a> were some of it is explained quite good by Jens Holger Rindel (one of the developers behind the acoustic software Odeon).</p>

<p>He explains among other things, that the decay rate in some rooms would not be a straight line after taking the logarithm of the exponential decay. You can in some circumstances get an exponential decay. This is due to the parallel walls in the x, y, and z direction. If the reverberation time varies much the sound energy would stay longer in the direction with the lowest absorption. If you imagine a plan wave coming towards a wall, some of the energy would be reflected in the two other directions due to scattering. The reverberation time curve would thus be effected most by the coupling between the directions with low reverberation time to the direction with the high reverberation time, and this would happen over the entire reverberation time curve, as the directions would have infinite many coupled parallel walls over time (this is similar to the problems regarding coupled rooms).</p>

<p>However, the calculation of the reverberation field is limited to the accuracy of the measured absorption coefficient, and that can vary as much as +/- 5%. So even if we had the perfect formula for reverberation time it wouldn&#39;t matter. This is by the way true for all physical prediction models as it depends strongly on the accuracy of the input - or GIGO (garbage in garbage out).</p>

<p>The implementation of all the formulas above is given in the code below:</p>

<pre lang="vbnet">
Dim FullOctaveBand() As String
Dim OctaveBandString As String = &quot;31,63,125,250,500,1000,2000,4000,8000,16000&quot;
FullOctaveBand = OctaveBandString.Split(&quot;,&quot;)

If Not _lstXAbsorbers.Count = 0 And Not _lstYAbsorbers.Count = 0 _
                      And Not _lstZAbsorbers.Count = 0 Then
    TotalSufraceArea = _TotalSurfaceArea

    Humidity = CInt(txtHumidityRoom.Text)

    Sabine.Name = &quot;Sabine&quot;
    Eyring.Name = &quot;Eyring&quot;
    Miller.Name = &quot;Millington-Sette&quot;
    Fritzroy.Name = &quot;Fritzroy&quot;

    Volume = _Height * _Length * _Width

    For i As Integer = 0 To Sabine.Count - 1

        Dim SabineTempZ, SabineTempY, SabineTempX As Double
        SabineTempX = 0
        SabineTempY = 0
        SabineTempZ = 0

        Dim EyringTempZ, EyringTempY, EyringTempX As Double
        EyringTempZ = 0
        EyringTempY = 0
        EyringTempX = 0

        Dim MillingtonSetteTempZ, MillingtonSetteTempY, MillingtonSetteTempX As Double
        MillingtonSetteTempZ = 0
        MillingtonSetteTempY = 0
        MillingtonSetteTempX = 0

        For Each p As Absorber In _lstZAbsorbers
            SabineTempZ += p.Area * p.GetItem(i)
            EyringTempZ += p.Area * p.GetItem(i) / _TotalSurfaceArea
            MillingtonSetteTempZ += p.Area * CalculateAlfa(p.GetItem(i))
        Next

        For Each p As Absorber In _lstYAbsorbers
            SabineTempY += p.Area * p.GetItem(i)
            EyringTempY += p.Area * p.GetItem(i) / _TotalSurfaceArea
            MillingtonSetteTempY += p.Area * CalculateAlfa(p.GetItem(i))
        Next
 
        For Each p As Absorber In _lstXAbsorbers
            SabineTempX += p.Area * p.GetItem(i)
            EyringTempX += p.Area * p.GetItem(i) / _TotalSurfaceArea
            MillingtonSetteTempX += p.Area * CalculateAlfa(p.GetItem(i))
        Next

        Dim m As Double = 5.5 * 10 ^ (-4) * (50 / Humidity) * _
                          (CDbl(FullOctaveBand(i)) / 1000) ^ (1.7)

        &#39;Calculationg total absorbtion area and air annutaion 
        Dim AbsorbtionAreaSabine As Double = (SabineTempX + SabineTempY + SabineTempZ) + 4 * m * Volume
        Dim AbsorbtionAreaEyring As Double = _TotalSurfaceArea * _
            CalculateAlfa(EyringTempX + EyringTempY + EyringTempZ) + 4 * m * Volume
        Dim AbsorbtionAreaMillingtonSette As Double = MillingtonSetteTempX + _
            MillingtonSetteTempY + MillingtonSetteTempZ + 4 * m * Volume


        Dim FritzroyTempZ, FritzroyTempY, FritzroyTempX As Double
        FritzroyTempX = CDbl(lblXrea.Content) / (CalculateAlfa(EyringTempX) + _
           (4 * m * Volume / 3) * (CDbl(lblXrea.Content) / _TotalSurfaceArea ^ 2))
        FritzroyTempY = CDbl(lblYrea.Content) / (CalculateAlfa(EyringTempY) + _
          (4 * m * Volume / 3) * (CDbl(lblYrea.Content) / _TotalSurfaceArea ^ 2))
        FritzroyTempZ = CDbl(lblZarea.Content) / (CalculateAlfa(EyringTempZ) + _
          (4 * m * Volume / 3) * (CDbl(lblZarea.Content) / _TotalSurfaceArea ^ 2))


        Sabine.SetItem(i, ReverberationTime(AbsorbtionAreaSabine))
        Eyring.SetItem(i, ReverberationTime(AbsorbtionAreaEyring))
        Miller.SetItem(i, ReverberationTime(AbsorbtionAreaMillingtonSette))
        Fritzroy.SetItem(i, (0.16 * Volume / (_TotalSurfaceArea ^ 2)) * _
          (FritzroyTempX + FritzroyTempY + FritzroyTempZ))
    Next

    Dim AverageReverberationTimeSabine As Double
    AverageReverberationTimeSabine = 0
    For i As Integer = 0 To Sabine.Count - 1
        AverageReverberationTimeSabine += Sabine.GetItem(i) / Sabine.Count
    Next
 
    &#39;Calculate the Schroeder frequency based on Sabine
    Schroeder = 2000 * Math.Sqrt(AverageReverberationTimeSabine / Volume)

    &#39;Room radius is calculated besed on a omnidirectional source, meaning D = 1
    RoomRadius = Math.Sqrt(55.26 * Volume / (16 * Math.PI * C0 * AverageReverberationTimeSabine))

    lblSchroder.Content = Math.Round(Schroeder, 0).ToString
    lblRoomRadius.Content = Math.Round(RoomRadius, 2).ToString

    _lstResult2.Add(Sabine)
    _lstResult2.Add(Eyring)
    _lstResult2.Add(Miller)
    _lstResult2.Add(Fritzroy)

    lstResult.ItemsSource = _lstResult2

    For Each p As Absorber In _lstResult2
        PlotReverbrationTime(p, False)
    Next

Else
    MessageBox.Show(&quot;missing absorbers&quot;)
End If</pre>

<h2>Comments on the formulas</h2>

<p>There are several comments to be given in relation to the result that is derived, and especially with regards to the limitations of the theory. The assumption was that all the points in the room had the exact same energy, which was used to formulate that it didn&#39;t matter where in the room the absorbing surfaces were. &nbsp;&nbsp;</p>

<p>There is actually a simple analogy from electrical circuit theory. The room can be modeled as a simple capacitor, and thus the absorption in a room as a simple resistor and the leakage from slits like a window, doors etc could be modeled as a inductor. It is evident that a &quot;normal&quot; room would behave as a bandpass filter with coefficient dependent on the acoustic properties of the boundaries and the geometry of the room. This means that we could model the energy in a room, based on the charge over a capacitor:&nbsp;</p>

<p><img height="200" src="rccurve.gif" width="450" />&nbsp;</p>

<p>&nbsp;</p>

<p>The steady state condition could be found between the charge and discharge, as is also true in a room. In the capacitor analogy reverberation time calculations is the same as the discharge in a capacitor. However the charge of the capacitor has also it&#39;s equivalent in room acoustics, and it&#39;s called early decay time (EDT). There are a couple of things missing from the analogy that is performed between the capacitor and a room. One is the direct sound, i.a. the shortest path between the listener and the source, the other is that the steady state in acoustics is a combination of three different phenomena, namely DirectSound, EDT and at last the reverberation time, while the capacitor is a combination of charge and discharge only. And 3D coupled walls is also not percent in a single capacitor. So the simulation of an impulse response squared with the different regions marked by different color is shown in the picture below:&nbsp;</p>

<p><img height="192" src="ImpulseResponse.jpg" width="340" />&nbsp;</p>

<p>In Fourier analysis, the room nodes or eigenvalues, are often used to acoustics to explain how the sound pressure is distributed through the room. The problem is that the walls are the sources with this assumtion, the same assumtion that was used to derive the reverberation time in Sabines equation, and would thereby not give you the correct values of sound pressure in a room with an arbitrary placement of the source.</p>

<p>The do however explain the influence of the walls in a room with the averaging of several measurement points in a room, were both the source and microphone position is varied. This is actually known information, and the Waterhouse calculated a formula based on the assumption, as the sound pressure at the low frequencies have to be fixed due to the influence of the low frequency nodes. &nbsp;</p>

<p>Up to this point the article only explains what happens in the reverberation section of the sound in the room. The direct sound is relatively easy to implement in a game, as it is the direct or shortest distance between source and receiver. The early reflections are very important if you want to simulate a sound field in a room, as most of the perceived spatial information is picked up here. This part is usually simulated with Image sources, or mirror sources. Normally the most important information comes from 0 to 80 ms after the direct sound, and EDT is defined in standards as the 10 dB decay from the first reflection. (In measurement it is defined as from the top -0.1 dB to -10.1 dB, as one tries to not include the effect of the direct sound.)&nbsp;</p>

<p>So if we cant find the sound pressure exactly what techniques could we use? Well as it turns out the only technique that could predict the sound field completely accurate is the Transmission Line Model (TLM) or the equivalent Finite Difference Time Dependent&nbsp; (FDTD), but the calculation time is directly dependent on frequency, and it is therefore not a viable <span class="st"> technique </span>for real time processes.</p>

<p>Geometrical acoustics is the analytic tool used by room acoustic consultants, and this is a hybrid solution with a combination of rays and image sources. Examples of programs used are:&nbsp;&nbsp;&nbsp;</p>

<ul>
	<li><a href="http://www.odeon.dk/">Odeon</a>&nbsp;</li>
	<li><a href="http://www.catt.se/">Catt</a>&nbsp;</li>
</ul>

<p>Those are the only two I know of and there might be other who offer commercial programs. They predict the sound pressure quite good at medium and high frequencies, but fail at the lower frequencies. This is becouse at low frequencies the sound don&#39;t move around as rays but more as plane waves. At the frequency were the ray transformation is valid, can be estimated using the Schroeder frequency. This says when our sound pressure is diffuse, and thus below this frequency we could expect the sound pressure to be dominated by the nodes generated by the boundaries.&nbsp;</p>

<p>However the information you&#39;ll get out of these programs is in many circumstances is a huge overkill for what is needed to roughly estimate a decay curve, that would sound&nbsp;believable in most cases. (This is not to say that they are not needed, because they are. Mostly in situations were you are going to spend millions of dollars constructing a symphony hall, huge lecture hall, theater, opera house etc., only to find out that the sound is messing up the impression leaving the audience/students in disarray. The ray tracing/image source <span class="st">technique </span>is usually also very reliable for frequencies were our hearing is most sensitive.)</p>

<h2>Auralization &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</h2>

<p>The name auralization derives directly from visualization, but&nbsp; sound is not visible but audible, therefore the name Auralization. It does however require quite a lot of processing to translate the calculated reverberation time into something the could be used to artificially create the sound in a room.</p>

<p>To create an audio of reasonable&nbsp; you need ot make an 1/3 octave band filter, that could be applied in the range from 31.5 Hz - 16 000 Hz (the program only calculates the full octave bands, but values can be usually be interpolated to cover 1/3 octaveband. The exception would be different kind of narrow Helmholt absorbers). This is not straightforward and requires knowledge of DSP (Digital signal processing) and filter design. A pseudo impulse response for rooms could be created as suggested below:&nbsp;</p>

<ol>
	<li>Get a white noise signal with sufficient length (approximately 4 seconds or more should be enough in most circumstances. (I will assume that this has values from 1 to -1, and I know wav files do not have that)</li>
	<li>Filter the signal in all octave bands and store it.</li>
	<li>Multiply the filtered signal by p(t) equation (or rather the e<sup>-xt</sup> signal) -10 dB. p(t) would be found in the different reverberation time formulas.</li>
	<li>Add the direct sound at the beginning with the value 1 (a delay of about 60 - 80 ms between the direct sound and the reverberation field would make it sound more natural, or better even to calculate the sound between the direct sound and the reverberation field using image sources).</li>
	<li>Take the sound (without reflections) that you want to play in the game and convolute this with the proposed filter.</li>
</ol>

<p>As you by now probably have guessed, making a game sound natural is quite a task, and I know that there are instances were this algorithm won&#39;t be advisable, as in corridors, outdoor or other areas that do not have a reverberant field (look at the pre-conditions that was set when deriving the equations).</p>

<p>A bit of warning before you use the software directly. I won&#39;t guarantee that the absorption coefficient supplied in the excel-file are correct, though they look like reasonable assumptions to me. But then again you can add all the items in the excel file that you could think of (or find other places), so there is no problem in that sense.</p>

<h2>History&nbsp;</h2>

<p>First relase 31.07.2012</p>

<p>The program includes:</p>

<ul>
	<li>Calculation of reverberation time (four different algorithms)</li>
	<li>Loading absorption data in full octave band from an editable Excel file</li>
</ul>

<h2>References&nbsp;&nbsp;</h2>

<p>The books I list here offers a good explanation of what I describe, and are good further references if you want to know more.&nbsp;&nbsp;</p>

<ul>
	<li><em>&quot;Building acoustics&quot;</em>, First edition (2008), Tor Erik Vigran, CRC Press&nbsp;</li>
	<li><em>&quot;Auralization - Fundamentals of Acoustics, Modelling, Simulation, Algorithms and Acoustic Virtual Reality&quot;</em>, First edition (2008), Michael Vorlander, Springer.</li>
	<li><em>&quot;Room Acoustics&quot;</em>, Fifth edition (2009), Henrich Kuttruff, Spon Press.&nbsp;</li>
	<li><em>&quot;Fundementals of Acoustics&quot;</em>, Fourth edition, Kinsler, Frey, Coppens and Sanders, John Wiley &amp; Sons, Inc.&nbsp;</li>
</ul>

<p>There is a book that just deals with reflections and absorption of materials, and if you want to know how to deal with theoretical calculations you should defiantly read this:&nbsp;&nbsp;</p>

<ul>
	<li><em>&quot;Acoustic Absorbers and diffuseres - Theory, design and application&quot;</em>, Second edition (2009), Trevor J. Cox and Peter D&#39;Antonio, Taylor &amp; Francis.&nbsp;&nbsp;&nbsp;</li>
</ul>

<p>A short history of different reverberation time formulas could be found here:</p>

<ul>
	<li><a href="http://sound.eti.pg.gda.pl/papers/prediction_of_reverberation_time.pdf">http://sound.eti.pg.gda.pl/papers/prediction_of_reverberation_time.pdf</a></li>
</ul>

<p>Some other interesting sites:</p>

<ul>
	<li><a href="http://webphysics.davidson.edu/faculty/dmb/py115/ReverbCalc.html%20">http://webphysics.davidson.edu/faculty/dmb/py115/ReverbCalc.html&nbsp;</a>&nbsp;</li>
	<li><a href="http://webphysics.davidson.edu/faculty/dmb/py115/ReverbCalc.html%20">http://www.esru.strath.ac.uk/Courseware/Acoustic/aco.htm</a></li>
</ul>
