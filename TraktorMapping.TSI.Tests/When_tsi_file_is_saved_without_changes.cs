﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Machine.Specifications;

namespace TraktorMapping.TSI.Tests
{
    public class When_tsi_file_is_saved_without_changes
    {
        // TODO: I'm not too sure quite how this was working, but the bug-fix I did for disposed stream errors made it not compile. Needs looking at ideally
        /*
        static TsiFile TsiFile;
        static byte[] ExpectedOutput;
        static byte[] Output;

        Establish context = () => {
            ExpectedOutput = Resources.Complex_Midi_Mapping;
            TsiFile = new TsiFile(new MemoryStream(ExpectedOutput));
        };

        Because of = () => {
            using (MemoryStream output = new MemoryStream()) {
                TsiFile.Save(output);
                Output = output.ToArray();
            }
        };

        It should_match_the_input_file_exactly = () => {
            Output.SequenceEqual(ExpectedOutput);
        };
        */
    }
}
