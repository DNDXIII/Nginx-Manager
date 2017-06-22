import React from 'react';
import RaisedButton from 'material-ui/RaisedButton';


export default class Terminal extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            textPos: 0
        };
    }

    onLineSubmit = (event) => {
        event.preventDefault();
        var line = document.getElementById("inputfield").value;
        document.getElementById("inputfield").value = "";
        this.props.handleWriteLine(line);
    }

    handleKeyPress = (event) => {
        var pos="";
        if (event.keyCode == 40) {
            pos = this.props.usertext.length - this.state.textPos;
            if (pos < this.props.usertext.length) {
                document.getElementById("inputfield").value = this.props.usertext[pos];
                this.setState({ textPos: this.state.textPos - 1 });
            }
        }
        else if (event.keyCode == 38) {
            pos = this.props.usertext.length - this.state.textPos;
            if (pos > 0) {
                document.getElementById("inputfield").value = this.props.usertext[pos - 1];
                this.setState({ textPos: this.state.textPos + 1 });
            }
        }
        else if (this.state.textPos != 0) {
            this.setState({ textPos: 0 });
        }
    }

    render() {
        return (
            <div>
                <pre id="textBox" style={{
                    fontSize: 17,
                    width: 1200,
                    height: 400,
                    overflow: "auto",
                    backgroundColor: "black",
                    color: "white",
                    fontFamily: "Lucida Console",
                    paddingBottom: "2%",
                    margin: 0
                }}>
                    {this.props.alltext()}
                </pre>
                <form autoComplete="off" onSubmit={this.onLineSubmit}>
                    <input type="text" onKeyDown={this.handleKeyPress} id="inputfield" style={{
                        width: 1200,
                        border: "none",
                        borderColor: "transparent",
                        backgroundColor: "grey",
                        color: "white",
                        fontFamily: "Lucida Console",
                        fontSize: 17
                    }} />

                    <button type="submit" style={{ display: "none" }} />
                </form>
                <RaisedButton onTouchTap={this.props.handleDisconnect} style={{ margin: 13, marginBottom: 20 }} label="Disconnect" />
            </div>
        );
    }
}